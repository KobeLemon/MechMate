using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MechMate.Models;

namespace MechMate.ViewModels;

public partial class AddEditVehiclePageViewModel : ObservableObject
{
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
