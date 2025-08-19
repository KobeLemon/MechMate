using CommunityToolkit.Mvvm.ComponentModel;
using MechMate.Interfaces;
using MechMate.Models;
using MechMate.Services;
using System.Collections.ObjectModel;

namespace MechMate.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Vehicle> vehicleList = new();

    private readonly string vehicleFile = "vehicles.json";
    private readonly IMongoDBService _mongoDbService;
    private readonly FileService _fileService;
    public MainPageViewModel(
        Services.MongoDBService mongoDbService, 
        Services.FileService fileService)
    {
        _mongoDbService = mongoDbService;
        _fileService = fileService;
    }

    public void OnPageAppearing()
    {
        LoadVehicleData();
    }

    private async void LoadVehicleData()
    {
        VehicleList = await _fileService.ReadJsonListAsync<Vehicle>(vehicleFile);
    }

    //private async void GetAllShortVehicleInfo()
    //{
    //    VehicleList = await _mongoDbService.GetAllShortInfoForGarage();
    //}
}
