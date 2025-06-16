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
        BindingContext = new MyRidePageViewModel(vehicle.Id);
    }

    private async void GoToMyRepairsPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MyRepairsPage(_vehicle.Id, _vehicle.DisplayName, _vehicle.VIN));
    }
}
