using MechMate.Models;
using MechMate.Services;
using MechMate.ViewModels;

namespace MechMate;

public partial class MyRidePage : ContentPage
{
    private readonly string _vehicleId;
    private readonly string _displayName;
    private readonly string _VIN;
    public MyRidePage(string vehicleId)
    {
        _vehicleId = vehicleId;
        InitializeComponent();
        BindingContext = new MyRidePageViewModel(vehicleId, new FileService(), Navigation);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MyRidePageViewModel vm)
            vm.OnPageAppearing();
    }
}
