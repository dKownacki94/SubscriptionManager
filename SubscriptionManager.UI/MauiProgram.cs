using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SubscriptionManager.Application.Interfaces;
using SubscriptionManager.Application.Services;
using SubscriptionManager.Domain.Interfaces;
using SubscriptionManager.Infrastructure.Repositories;
using SubscriptionManager.Infrastructure.Services;
using SubscriptionManager.UI.ViewModels;
using SubscriptionManager.UI.Views;

namespace SubscriptionManager.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddScoped<ISubscriptionRepository, InMemorySubscriptionRepository>();
        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        builder.Services.AddSingleton<IAvatarService, AvatarService>();

        builder.Services.AddTransient<SubscriptionListPage>();
        builder.Services.AddTransient<SubscriptionListViewModel>();

        builder.Services.AddTransient<SubscriptionEditPage>();
        builder.Services.AddTransient<SubscriptionEditViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
