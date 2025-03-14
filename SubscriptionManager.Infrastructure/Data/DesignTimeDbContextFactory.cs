using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SubscriptionManager.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SubscriptionDbContext>
{
    public SubscriptionDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SubscriptionDbContext>();

        optionsBuilder.UseSqlite("Data Source=subscriptions_design.db");

        return new SubscriptionDbContext(optionsBuilder.Options);
    }
}