using CommunityToolkit.Mvvm.ComponentModel;
using MechMate.Models;
using System.Collections.ObjectModel;
using Syncfusion.Pdf;
using Syncfusion.HtmlConverter;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using MechMate.Services;

namespace MechMate.ViewModels;

public partial class MyRidePageViewModel : ObservableObject
{
    [ObservableProperty]
    public Vehicle singleVehicle;

    private readonly string _singleVehicleId = string.Empty;
    private INavigation _navigation;
    private string _htmlTemplate = string.Empty;
    private readonly FileService _fileService;
    private readonly string vehicleFile = "vehicles.json";

    public MyRidePageViewModel(string singleVehicleId, FileService fileService, INavigation navigation)
    {
        _singleVehicleId = singleVehicleId;
        _fileService = fileService;
        _navigation = navigation;
    }

    public void OnPageAppearing()
    {
        LoadVehicleData();
    }

    private async void LoadVehicleData()
    {
        var vehicleList = await _fileService.ReadJsonListAsync<Vehicle>(vehicleFile);
        SingleVehicle = vehicleList.FirstOrDefault(x => x.Id == _singleVehicleId);
    }

    [RelayCommand]
    private async Task EditVehicle()
    {
        await _navigation.PushAsync(new AddEditVehiclePage(SingleVehicle));
    }

    [RelayCommand]
    private async Task GoToMyRepairsPage()
    {
        await _navigation.PushAsync(new MyRepairsPage(SingleVehicle.Id, SingleVehicle.DisplayName, SingleVehicle.VIN));
    }

    //[RelayCommand]
    //private async void ExportVehicleToPdf()
    //{
    //    _htmlTemplate = $@"<!DOCTYPE html><html lang=""en""><head><meta charset=""UTF-8""><meta name=""viewport"" content=""width=device-width, initial-scale=1.0""><title>2014 Nissan Versa</title><style>body{{font-family:Arial,sans-serif;padding:5rem;line-height:1.4;color:#333}}.container{{display:flex;max-width:100%;flex-direction:column;align-items:center}}.title{{text-align:center;font-size:24px;font-weight:bold;margin-bottom:20px;padding-bottom:10px;border-bottom:2px solid #333}}.vehicle-image{{text-align:center;margin:20px 0}}.vehicle-image img{{max-height:150px;width:auto;border-radius:8px}}.specifications{{width:100%;border-collapse:collapse;margin-top:20px}}.specifications tr{{border-bottom:1px solid #ddd}}.specifications td{{padding:12px 0;vertical-align:top}}.specifications td:first-child{{font-weight:bold;width:30%}}.specifications td:last-child{{text-align:right;width:70%}}</style></head><body><div class=""container""><h1 class=""title"">{SingleVehicle.DisplayName}</h1><div class=""vehicle-image""><img src=""{SingleVehicle.ImageBase64}"" alt=""{SingleVehicle.DisplayName}""/></div><table class=""specifications""><tr><td>Year</td><td>{SingleVehicle.Year}</td></tr><tr><td>Make</td><td>{SingleVehicle.Make}</td></tr><tr><td>Model</td><td>{SingleVehicle.Model}</td></tr><tr><td>Engine</td><td>{SingleVehicle.Engine}</td></tr><tr><td>VIN</td><td>{SingleVehicle.VIN}</td></tr><tr><td>Plate Number</td><td>{SingleVehicle.PlateNumber}</td></tr><tr><td>Color</td><td>{SingleVehicle.Color}</td></tr><tr><td>Body Type</td><td>{SingleVehicle.BodyType}</td></tr><tr><td>Fuel Type</td><td>{SingleVehicle.FuelType}</td></tr><tr><td>Transmission</td><td>{SingleVehicle.Transmission}</td></tr></table></div></body></html>";
    //    HtmlToPdfConverter htmlToPdfConverter = new();
    //    PdfDocument pdfDocument = htmlToPdfConverter.Convert(_htmlTemplate);
    //    MemoryStream memoryStream = new MemoryStream();
    //    pdfDocument.Save(memoryStream);
    //    pdfDocument.Close(true);
    //    await SavePdfStreamToAndroid(memoryStream, $"{Vehicle.Year}_{Vehicle.Make}_{Vehicle.Model}_saved.pdf");
    //}

    //private async Task SavePdfStreamToAndroid(MemoryStream stream, string filename)
    //{
    //        string documentsPath = FileSystem.DocumentsDirectory;
    //        string fullPath = Path.Combine(documentsPath, filename);

    //        using FileStream fileStream = File.Create(fullPath);
    //        stream.Position = 0;
    //        await stream.CopyToAsync(fileStream);

    //    }
}
