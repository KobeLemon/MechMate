using MechMate.Models;
using MechMate.ViewModels;

namespace MechMate

    public MainPage(MongoDBService mongoDBService)
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private async void OnVehicleSelectedAsync(object sender, SelectionChangedEventArgs e)
        {
            var selectedVehicle = e.CurrentSelection.FirstOrDefault() as Vehicle;
            if (selectedVehicle == null)
                return;
            await Navigation.PushAsync(new MyRidePage(selectedVehicle));
        }
    }
}
