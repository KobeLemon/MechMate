using MechMate.ViewModels;

namespace MechMate;

public partial class MyRepairsPage : ContentPage
{
    public MyRepairsPage(string vehicleID, string displayName)
    {
        InitializeComponent();
        BindingContext = new MyRepairsPageViewModel(vehicleID, displayName);

    }
}
