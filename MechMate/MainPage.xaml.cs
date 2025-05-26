using MechMate.ViewModels;

namespace MechMate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(this.Navigation);
        }

        //private async void GoToMyRidePage(object? sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new MyRidePage("123"));
        //}
    }
}
