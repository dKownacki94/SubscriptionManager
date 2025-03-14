using Microsoft.EntityFrameworkCore;
using SubscriptionManager.Infrastructure.Data;

namespace SubscriptionManager.UI;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();

        using (var scope = IPlatformApplication.Current.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();
            dbContext.Database.Migrate();
        }

        MainPage = new AppShell();
    }
}
