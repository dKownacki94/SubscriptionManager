using Microsoft.Extensions.Logging;
using SubscriptionManager.Services;
using SubscriptionManager.ViewModels;
using SubscriptionManager.Views;

namespace SubscriptionManager
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Rejestracja usług
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<NotificationService>();

            // Rejestracja ViewModeli
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<SubscriptionDetailViewModel>();

            // Rejestracja widoków
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<SubscriptionDetailPage>();


            return builder.Build();
        }
    }
}
