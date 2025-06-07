using MechMate.ViewModels;

namespace MechMate;

public partial class MyRepairsPage : ContentPage
{
    public MyRepairsPage(string vehicleId, string displayName, string VIN)
    {
        InitializeComponent();
        BindingContext = new MyRepairsPageViewModel(vehicleId, displayName, VIN);
    }
}
