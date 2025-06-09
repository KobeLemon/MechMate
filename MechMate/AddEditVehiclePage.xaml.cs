using MechMate.Models;
using MechMate.ViewModels;

namespace MechMate
{
    public partial class AddEditVehiclePage
    {
        public AddEditVehiclePage(Vehicle vehicle)
        {
            InitializeComponent();
            BindingContext = new AddEditVehiclePageViewModel(vehicle);
        }
    }
}
