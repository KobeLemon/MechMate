using MechMate.ViewModels;

namespace MechMate;

public partial class MyRidePage : ContentPage
{
    public MyRidePage(string vehicleID)
    {
        InitializeComponent();
        BindingContext = new MyRidePageViewModel(vehicleID, this.Navigation);
    }

    private void GoToMyRepairsPage(object sender, EventArgs e)
    {

    }
}
