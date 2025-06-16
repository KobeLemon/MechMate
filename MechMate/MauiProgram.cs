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
            DotNetEnv.Env.Load();
            var MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
            if (string.IsNullOrEmpty(MONGODB_CONNECTION_STRING))
            {
                throw new InvalidOperationException("Connection string is not set. Please ensure the MONGODB_CONNECTION_STRING environment variable is defined.");
            }

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();
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

            // Add MongoDB service
            builder.Services.AddSingleton<MongoDBService>(sp => 
                new MongoDBService(
                    "mongodb+srv://user1:<mechmate>@mechmate.sxdkq8x.mongodb.net/?retryWrites=true&w=majority&appName=MechMate",
                    "MechMateDB"
                ));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
