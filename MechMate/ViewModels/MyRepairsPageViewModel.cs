using CommunityToolkit.Mvvm.ComponentModel;

namespace MechMate.ViewModels;

public partial class MyRepairsPageViewModel : ObservableObject
{
    [ObservableProperty]
    public string _vehicleId = string.Empty;
    [ObservableProperty]
    public string _vehicleName = string.Empty;
    [ObservableProperty]
    public string _vehicleVIN = string.Empty;

    public MyRepairsPageViewModel(string vehicleId, string vehicleName, string vehicleVIN)
    {
        _vehicleId = vehicleId;
        _vehicleName = vehicleName;
        _vehicleVIN = vehicleVIN;
    }
}
