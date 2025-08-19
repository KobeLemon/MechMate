using CommunityToolkit.Maui;
using MechMate.Services;
using MechMate.ViewModels;
using Microsoft.Extensions.Logging;

namespace MechMate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MauiMaterialAssets.ttf", "MaterialAssets");
                })
                .UseMauiCommunityToolkit();
            // 
            string MONGODB_CONNECTION_STRING = "mongodb://user1:mechmate@mechmate.sxdkq8x.mongodb.net/?connectTimeoutMS=60000&retryWrites=true&w=majority&appName=MechMate";
            if (string.IsNullOrEmpty(MONGODB_CONNECTION_STRING))
            {
                throw new InvalidOperationException("Connection string is not set. Please ensure the MONGODB_CONNECTION_STRING environment variable is defined.");
            }

            builder.Services
                .AddSingleton(new MongoDBService(MONGODB_CONNECTION_STRING, "MechMateDB"))
                .AddSingleton(new FileService())
                .AddSingleton<VinLookupService>()
                .AddTransient<MainPage>()
                .AddTransient<MainPageViewModel>()
                .AddTransient<MyRidePage>()
                .AddTransient<MyRidePageViewModel>()
                .AddTransient<MyRepairsPage>()
                .AddTransient<MyRepairsPageViewModel>()
                .AddTransient<AddEditVehiclePage>()
                .AddTransient<AddEditVehiclePageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
