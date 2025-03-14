using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Infrastructure.DataModels;

namespace SubscriptionManager.Infrastructure.Mappers;

public static class SubscriptionMapper
{
    public static SubscriptionDto ToDto(Subscription domainModel)
    {
        return new SubscriptionDto
        {
            Id = domainModel.Id,
            Name = domainModel.Name,
            DateFrom = domainModel.DateFrom,
            DateTo = domainModel.DateTo,
            Price = domainModel.Price,
            AvatarPath = domainModel.AvatarPath
        };
    }

    public static Subscription ToDomain(SubscriptionDto dto)
    {
        var subscription = new Subscription(
            dto.Name,
            dto.DateFrom,
            dto.DateTo,
            dto.Price,
            dto.AvatarPath);

        typeof(Subscription)
            .GetProperty("Id")
            .SetValue(subscription, dto.Id);

        return subscription;
    }
}