using MechMate.Models;
using MechMate.ViewModels;

namespace MechMate
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is MainPageViewModel vm)
                vm.OnPageAppearing();
        }

        private async void OnVehicleSelectedAsync(object sender, SelectionChangedEventArgs e)
        {
            var selectedVehicle = e.CurrentSelection.FirstOrDefault() as Vehicle;
            if (selectedVehicle == null)
                return;
            await Navigation.PushAsync(new MyRidePage(selectedVehicle.Id));
        }

        private async void AddNewVehicle(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditVehiclePage(new Vehicle()));
        }
    }
}
