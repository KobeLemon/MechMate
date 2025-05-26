using MechMate.ViewModels;

namespace MechMate;

public partial class MyRepairsPage : ContentPage
{
    public MyRepairsPage(string vehicleID)
    {
        InitializeComponent();
        BindingContext = new MyRepairsPageViewModel();

    }
}
