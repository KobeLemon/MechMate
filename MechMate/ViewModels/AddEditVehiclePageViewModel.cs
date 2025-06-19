using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MechMate.Models;
using MechMate.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.RegularExpressions; // for ImageSource

namespace MechMate.ViewModels;

public partial class AddEditVehiclePageViewModel : ObservableObject
{
    private readonly VinLookupService _vinLookupService;
    private readonly string vehicleFile = "vehicles.json";
    private readonly FileService _fileService;

    [ObservableProperty]
    private string vinInput = string.Empty;
    [ObservableProperty]
    private string lookupResult = string.Empty;
    [ObservableProperty]
    public ImageSource vehicleImage;
    [ObservableProperty]
    public ObservableCollection<DisplayItem> displayVehicle = new();
    [ObservableProperty]
    public string failedToSave = string.Empty;

    private Vehicle _vehicle = new();
    private readonly INavigation _navigation;

    // Constructor for service injection
    public AddEditVehiclePageViewModel(Vehicle vehicle, VinLookupService vinLookupService, FileService fileService, INavigation navigation)
    {
        _vehicle = vehicle;
        DisplayVehicle = _vehicle.DisplayVehicle;
        VehicleImage = vehicle.ImageUrl;
        _vinLookupService = vinLookupService;
        _fileService = fileService;
        _navigation = navigation;
    }

    [RelayCommand]
    public async Task LookupVin()
    {
        if (string.IsNullOrWhiteSpace(VinInput))
        {
            LookupResult = "Please enter a VIN.";
            return;
        }

        string regexPattern = @"^[^IOQioq\W_]$";
        Regex regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
        Match match = regex.Match(VinInput);
        if (VinInput.Length != 17 || match.Success)
        {
            LookupResult = "Invalid VIN. Must be 17 digits & not contain letters I, O, or Q";
            return;
        }

        var result = await _vinLookupService.LookupVinAsync(VinInput);

        if (result == null)
        {
            LookupResult = "Lookup failed. Please try again.";
            return;
        }

        if (!string.IsNullOrEmpty(result.Message))
        {
            LookupResult = result.Message;
            return;
        }

        if (result.Results == null || !result.Results.Any())
        {
            LookupResult = "No data found for the given VIN.";
            return;
        }

        // Format the result nicely
        LookupResult = string.Join("\n", result.Results.Select(r => $"{r.Variable}: {r.Value}"));
    }

    [RelayCommand]
    private async Task SelectFile()
    {
        var file = await FilePicker.Default.PickAsync();
        if (file != null)
        {
            VehicleImage = ImageSource.FromFile(file.FullPath);
        }
    }

    [RelayCommand]
    private async Task SaveVehicle()
    {
        foreach (var item in DisplayVehicle)
        {
            string trimmedKey = item.Key.Replace(" ", "");

            PropertyInfo propertyInfo = _vehicle.GetType().GetProperty(trimmedKey);
            string propertyValue = propertyInfo.GetValue(_vehicle).ToString();

            if (propertyInfo != null && propertyInfo.CanWrite && propertyValue != item.Value)
            {
                if (item.Key.Equals("YEAR", StringComparison.OrdinalIgnoreCase) && int.TryParse(item.Value, out int parsedYear))
                    propertyInfo.SetValue(_vehicle, parsedYear);
                else
                    propertyInfo.SetValue(_vehicle, item.Value);
            }
        }

        string newImageBase64;

        if (VehicleImage is FileImageSource fileSource)
        {
            byte[] bytes = await File.ReadAllBytesAsync(fileSource.File);
            _vehicle.ImageBase64 = Convert.ToBase64String(bytes);
        }

        if (VehicleImage is StreamImageSource streamSource)
        {
            using var stream = await streamSource.Stream(CancellationToken.None);
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            byte[] msArray = ms.ToArray();
            _vehicle.ImageBase64 = Convert.ToBase64String(msArray);
        }

        ObservableCollection<Vehicle> vehiceList = await _fileService.ReadJsonListAsync<Vehicle>(vehicleFile);

        var itemIndex = vehiceList
            .Select((item, index) => new { item, index })
            .FirstOrDefault(x => x.item.Id == _vehicle.Id).index;

        if (itemIndex >= 0)
            vehiceList[itemIndex] = _vehicle;
        else
            vehiceList.Add(_vehicle);

        await _fileService.WriteJsonAsync<Vehicle>(vehiceList, vehicleFile);
        await _navigation.PushAsync(new MyRidePage(_vehicle.Id));
    }
}
