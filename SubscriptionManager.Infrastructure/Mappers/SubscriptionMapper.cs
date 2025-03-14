using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Infrastructure.DataModels;

namespace SubscriptionManager.Infrastructure.Mappers;

public static class SubscriptionMapper
{
    public static SubscriptionEntity ToEntity(Subscription domainModel)
    {
        return new SubscriptionEntity
        {
            Id = domainModel.Id,
            Name = domainModel.Name,
            DateFrom = domainModel.DateFrom,
            DateTo = domainModel.DateTo,
            Price = domainModel.Price,
            AvatarPath = domainModel.AvatarPath
        };
    }

    public static Subscription ToDomain(SubscriptionEntity entity)
    {
        var subscription = new Subscription(
            entity.Name,
            entity.DateFrom,
            entity.DateTo,
            entity.Price,
            entity.AvatarPath);

        typeof(Subscription)
            .GetProperty("Id")
            .SetValue(subscription, entity.Id);

        return subscription;
    }
}