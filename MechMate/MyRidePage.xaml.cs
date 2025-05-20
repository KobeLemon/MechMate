using MechMate.ViewModel;

namespace MechMate;

public partial class MyRide : ContentPage
{
	public MyRide(MyRidePageViewModel myRidePageViewModel)
	{
		InitializeComponent();
		BindingContext = myRidePageViewModel;
	}
}