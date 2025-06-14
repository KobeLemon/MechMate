using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MechMate.Models;
using MechMate.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls; // for ImageSource

namespace MechMate.ViewModels;

public partial class AddEditVehiclePageViewModel : ObservableObject
{
    private readonly VinLookupService _vinLookupService;

    [ObservableProperty]
    private string vinInput = string.Empty;

    [ObservableProperty]
    private string lookupResult = string.Empty;

    [ObservableProperty]
    public Vehicle _vehicle = new();

    [ObservableProperty]
    public ImageSource vehicleImage;

    [ObservableProperty]
    public string title = "Add or Edit Vehicle";

    // Constructor for service injection
    public AddEditVehiclePageViewModel(Vehicle vehicle, VinLookupService vinLookupService)
    {
        _vehicle = vehicle;
        VehicleImage = vehicle.ImageUrl;
        _vinLookupService = vinLookupService;
    }

    // Optional constructor when adding a new vehicle without an existing one
    public AddEditVehiclePageViewModel(VinLookupService vinLookupService)
    {
        _vinLookupService = vinLookupService;
        _vehicle = new Vehicle();
    }


    [RelayCommand]
    public async Task LookupVinAsync()
    {
        if (string.IsNullOrWhiteSpace(VinInput))
        {
            LookupResult = "Please enter a VIN.";
            return;
        }

        var result = await _vinLookupService.LookupVinAsync(VinInput);

        if (result == null)
        {
            LookupResult = "Lookup failed.";
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
}
