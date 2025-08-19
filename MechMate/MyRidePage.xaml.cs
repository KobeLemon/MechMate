using MechMate.Services;
using MechMate.ViewModels;

namespace MechMate;

public partial class MyRidePage : ContentPage
{
    public MyRidePage(string vehicleId)
    {
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
