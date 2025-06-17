using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MechMate.Models;
using MechMate.Services;
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
    public Vehicle _vehicle = new();
    [ObservableProperty]
    public ImageSource vehicleImage;

    private readonly INavigation _navigation;

    // Constructor for service injection
    public AddEditVehiclePageViewModel(Vehicle vehicle, VinLookupService vinLookupService, FileService fileService)
    {
        _vehicle = vehicle;
        VehicleImage = vehicle.ImageUrl;
        _vinLookupService = vinLookupService;
        _fileService = fileService;
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
        List<Vehicle> vehiceList = await _fileService.ReadJsonListAsync<Vehicle>(vehicleFile);

        Vehicle = new()
        {
            Id = Vehicle.Id,
            
        };

        var itemIndex = vehiceList.FindIndex(x => x.Id == Vehicle.Id);

        if (itemIndex >= 0)
            vehiceList[itemIndex] = Vehicle;
        else
            vehiceList.Add(Vehicle);

        await _fileService.WriteJsonAsync(vehiceList, vehicleFile);
        await _navigation.PushAsync(new MyRidePage(Vehicle));
    }
}
