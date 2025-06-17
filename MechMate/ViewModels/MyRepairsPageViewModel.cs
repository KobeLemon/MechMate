using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using MechMate.Models; // Assuming RepairInstance is in this namespace
using MechMate.Services; // Assuming MongoDBService is in this namespace

namespace MechMate.ViewModels;

public partial class MyRepairsPageViewModel : ObservableObject
{
    private readonly MongoDBService _mongoDBService; // Inject your service

    [ObservableProperty]
    public string _vehicleId = string.Empty;
    [ObservableProperty]
    public string _vehicleName = string.Empty;
    [ObservableProperty]
    public string _vehicleVIN = string.Empty;

    public ObservableCollection<RepairInstance> Repairs { get; set; } = new ObservableCollection<RepairInstance>();

    public MyRepairsPageViewModel(string vehicleId, string vehicleName, string vehicleVIN, MongoDBService mongoDBService) // Inject the service
    {
        VehicleId = vehicleId;
        VehicleName = vehicleName;
        VehicleVIN = vehicleVIN;
        _mongoDBService = mongoDBService;
        LoadRepairsCommand = new AsyncRelayCommand(LoadRepairsAsync); // Command to load repairs
    }

    [RelayCommand] // Use the [RelayCommand] attribute for simpler command creation
    public async Task LoadRepairsAsync()
    {
        // Clear existing repairs
        Repairs.Clear();

        // Load repairs from MongoDB (you'll need a method in your service for this)
        var repairs = await _mongoDBService.GetRepairsForVehicleAsync(VehicleId); // Example method call

        // Add loaded repairs to the collection
        foreach (var repair in repairs)
        {
            Repairs.Add(repair);
        }
    }

    public IAsyncRelayCommand LoadRepairsCommand { get; } // Add the command property
}
