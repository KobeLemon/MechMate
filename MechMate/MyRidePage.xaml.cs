using Mechmate.Models;
using MechMate.ViewModels;

namespace MechMate;

public partial class MyRidePage : ContentPage
{
    public MyRidePage()
    {
        InitializeComponent();
        BindingContext = new MyRidePageViewModel();
    }

    Vehicle vehicle = new();

    private async void GoToMyRepairsPage(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new MyRepairsPage(vehicle.Id));
    }
}
