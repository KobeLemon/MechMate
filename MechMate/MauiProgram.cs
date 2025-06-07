using CommunityToolkit.Maui;
using MechMate.ViewModels;
using MechMate.Services;
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
                })
                .UseMauiCommunityToolkit();
            // 
            string MONGODB_CONNECTION_STRING = "mongodb+srv://user1:mechmate@mechmate.sxdkq8x.mongodb.net/?retryWrites=true&w=majority&appName=MechMate";
            if (string.IsNullOrEmpty(MONGODB_CONNECTION_STRING))
            {
                throw new InvalidOperationException("Connection string is not set. Please ensure the MONGODB_CONNECTION_STRING environment variable is defined.");
            }
            builder.Services
                .AddTransient<MainPage>()
                .AddTransient<MainPageViewModel>()
                .AddTransient<MyRidePage>()
                .AddTransient<MyRidePageViewModel>()
                .AddTransient<MyRepairsPage>()
                .AddTransient<MyRepairsPageViewModel>()
                .AddSingleton<MongoDBService>(service =>
                new MongoDBService(
                    MONGODB_CONNECTION_STRING,
                    "MechMateDB"
                ));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
