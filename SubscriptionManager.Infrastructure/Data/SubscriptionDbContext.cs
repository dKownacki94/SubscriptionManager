using Microsoft.EntityFrameworkCore;
using SubscriptionManager.Infrastructure.DataModels;

namespace SubscriptionManager.Infrastructure.Data;

public class SubscriptionDbContext : DbContext
{
    public DbSet<SubscriptionEntity> Subscriptions { get; set; }

    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubscriptionEntity>(entity =>
        {
            entity.ToTable("Subscriptions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.AvatarPath);
        });
    }
}