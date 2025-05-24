using MechMate.ViewModels;

namespace MechMate;

public partial class RepairsPage : ContentPage
{
    public RepairsPage()
    {
        InitializeComponent();
        BindingContext = new RepairsPageViewModel();

    }
}
