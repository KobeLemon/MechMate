using MechMate.New.Models;

namespace MechMate.New;

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
			ConnectionStatusLabel.Text = "Testing connection...";
			bool isConnected = await _connectionTest.TestConnection();
			if (isConnected)
			{
				ConnectionStatusLabel.Text = "Connected to MongoDB Atlas!";
				await DisplayAlert("Success", "Successfully connected to MongoDB Atlas!", "OK");
			}
			else
			{
				ConnectionStatusLabel.Text = "Failed to connect to MongoDB Atlas";
				await DisplayAlert("Error", "Failed to connect to MongoDB Atlas. Please check your connection string and network settings.", "OK");
			}
		}
		catch (Exception ex)
		{
			ConnectionStatusLabel.Text = "Error occurred";
			await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
		}
	}
}

