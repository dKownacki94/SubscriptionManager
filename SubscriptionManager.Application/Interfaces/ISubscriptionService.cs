using SubscriptionManager.Application.DTOs;

namespace SubscriptionManager.Application.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
    Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id);
    Task AddSubscriptionAsync(SubscriptionDto subscriptionDto);
    Task UpdateSubscriptionAsync(SubscriptionDto subscriptionDto);
    Task DeleteSubscriptionAsync(Guid id);
}