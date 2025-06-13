using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MechMate.Models;
using MechMate.Services;

namespace MechMate.ViewModels;

public partial class AddEditVehiclePageViewModel : ObservableObject
{
     private readonly VinLookupService _vinLookupService;

    [ObservableProperty]
    private string vinInput = string.Empty;

    [ObservableProperty]
    private string lookupResult = string.Empty;

    //public VinLookupViewModel(VinLookupService vinLookupService)
    //{
    //    _vinLookupService = vinLookupService;
    //}

    [RelayCommand]
    public async Task LookupVinAsync()
    {
        if (string.IsNullOrWhiteSpace(VinInput))
        {
            LookupResult = "Please enter a VIN.";
            return;
        }

        var result = await _vinLookupService.LookupVinAsync(VinInput);
        LookupResult = result?.Message ?? "Lookup failed.";
    }

    [ObservableProperty]
    public Vehicle _vehicle = new();
    [ObservableProperty]
    public ImageSource vehicleImage;
    [ObservableProperty]
    public string title = "Add or Edit Vehicle";

    public AddEditVehiclePageViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;
        VehicleImage = vehicle.ImageUrl;
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
