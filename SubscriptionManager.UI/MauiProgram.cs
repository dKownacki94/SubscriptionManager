using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PanCardView;
using SubscriptionManager.Application.Interfaces;
using SubscriptionManager.Application.Mapping;
using SubscriptionManager.Application.Services;
using SubscriptionManager.Domain.Interfaces;
using SubscriptionManager.Infrastructure.Data;
using SubscriptionManager.Infrastructure.Mapping;
using SubscriptionManager.Infrastructure.Repositories;
using SubscriptionManager.Infrastructure.Services;
using SubscriptionManager.UI.Services;
using SubscriptionManager.UI.ViewModels;
using SubscriptionManager.UI.Views;
using Microsoft.EntityFrameworkCore;

namespace SubscriptionManager.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseCardsView()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IFileSystemService, FileSystemService>();
        builder.Services.AddSingleton<IDbPathProvider, DbPathProvider>();

        builder.Services.AddDbContext<SubscriptionDbContext>((services, options) =>
        {
            var dbPath = services.GetRequiredService<IDbPathProvider>().GetDbPath();
            options.UseSqlite($"Data Source={dbPath}");
        }, ServiceLifetime.Scoped);

        builder.Services.AddScoped<ISubscriptionRepository, SQLiteSubscriptionRepository>();

        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        builder.Services.AddSingleton<IAvatarService, AvatarService>();

        builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile).Assembly, typeof(InfrastructureMappingProfile).Assembly);

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