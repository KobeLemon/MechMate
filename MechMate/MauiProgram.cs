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
                .AddSingleton<MainPage>()
                .AddSingleton<MainPageViewModel>()
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
