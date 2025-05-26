using Microsoft.Extensions.Logging;
using MechMate.Models;

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
