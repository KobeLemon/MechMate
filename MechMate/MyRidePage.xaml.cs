using MechMate.Models;
using MechMate.ViewModels;

namespace MechMate;

public partial class MyRidePage : ContentPage
{
    private readonly Vehicle _vehicle;
    public MyRidePage(Vehicle vehicle)
    {
        _vehicle = vehicle;
        InitializeComponent();
        // This is commented out because we need this once the mongo data is coming through with just the id. Full vehicle info is just for testing.
        //BindingContext = new MyRidePageViewModel(vehicle.Id);
        BindingContext = new MyRidePageViewModel(vehicle);
    }

    private async void GoToMyRepairsPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MyRepairsPage(_vehicle.Id, _vehicle.DisplayName, _vehicle.VIN));
    }

    private async void EditVehicle(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEditVehiclePage(_vehicle));
    }
}
