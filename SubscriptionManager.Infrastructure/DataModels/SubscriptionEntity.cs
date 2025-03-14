namespace SubscriptionManager.Infrastructure.DataModels;

public class SubscriptionEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public decimal Price { get; set; }
    public string AvatarPath { get; set; }
}