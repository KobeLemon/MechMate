using MechMate.ViewModels;

namespace MechMate;

public partial class MyRidePage : ContentPage
{
    public MyRidePage()
    {
        InitializeComponent();
        BindingContext = new MyRidePageViewModel();

    }
}
