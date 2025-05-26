using CommunityToolkit.Mvvm.ComponentModel;

namespace MechMate.ViewModels;

public partial class MyRepairsPageViewModel : ObservableObject
{
    [ObservableProperty]
    public string vehicleId = string.Empty;
    [ObservableProperty]
    public string vehicleName = string.Empty;
    public MyRepairsPageViewModel(string _vehicleId, string _vehicleName)
    {
        vehicleId = _vehicleId;
        vehicleName = _vehicleName;
    }
}
