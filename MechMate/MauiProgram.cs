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
                });
            builder.Services
                .AddSingleton<MainPage>()
                .AddSingleton<MainPageViewModel>()
                .AddSingleton<MyRidePage>()
                .AddSingleton<MyRidePageViewModel>()
                .AddSingleton<RepairsPage>()
                .AddSingleton<RepairsPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
