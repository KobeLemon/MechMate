using MechMate.Models;
using MechMate.ViewModels;

namespace MechMate
{
    public partial class AddEditVehiclePage
    {
        public AddEditVehiclePage(Vehicle vehicle)
        {
            InitializeComponent();
            var vinLookupService = new VinLookupService();
            BindingContext = new AddEditVehiclePageViewModel(vehicle, vinLookupService);
        }
        // Constructor for adding a new vehicle
        public AddEditVehiclePage()
        {
            InitializeComponent();
            var vinLookupService = new VinLookupService();
            BindingContext = new AddEditVehiclePageViewModel(vinLookupService);
        }

    }
}
