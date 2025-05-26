using MechMate.Models;

namespace MechMate;

public partial class MainPage : ContentPage
{
    private readonly MongoDBConnectionTest _connectionTest;

    public MainPage(MongoDBService mongoDBService)
    {
        InitializeComponent();
        _connectionTest = new MongoDBConnectionTest(mongoDBService);
        TestMongoDBConnection();
    }

    private async void TestMongoDBConnection()
    {
        try
        {
            bool isConnected = await _connectionTest.TestConnection();
            if (isConnected)
            {
                await DisplayAlert("Success", "Successfully connected to MongoDB Atlas!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to connect to MongoDB Atlas. Please check your connection string and network settings.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
