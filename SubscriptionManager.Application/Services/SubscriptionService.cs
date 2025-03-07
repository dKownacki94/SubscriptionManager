using SubscriptionManager.Application.DTOs;
using SubscriptionManager.Application.Interfaces;
using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Domain.Interfaces;

namespace SubscriptionManager.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.GetAllAsync();
        var currentDate = DateTime.Today;

        return subscriptions.Select(s => new SubscriptionDto
        {
            Id = s.Id,
            Name = s.Name,
            DateFrom = s.DateFrom,
            DateTo = s.DateTo,
            Price = s.Price,
            AvatarPath = s.AvatarPath,
            Status = MapDomainStatusToDto(s.GetStatus(currentDate))
        });
    }

    public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(id);
        if (subscription == null)
            return null;

        var currentDate = DateTime.Today;

        return new SubscriptionDto
        {
            Id = subscription.Id,
            Name = subscription.Name,
            DateFrom = subscription.DateFrom,
            DateTo = subscription.DateTo,
            Price = subscription.Price,
            AvatarPath = subscription.AvatarPath,
            Status = MapDomainStatusToDto(subscription.GetStatus(currentDate))
        };
    }

    public async Task AddSubscriptionAsync(SubscriptionDto subscriptionDto)
    {
        var subscription = new Subscription(
            subscriptionDto.Name,
            subscriptionDto.DateFrom,
            subscriptionDto.DateTo,
            subscriptionDto.Price,
            subscriptionDto.AvatarPath);

        await _subscriptionRepository.AddAsync(subscription);
    }

    public async Task UpdateSubscriptionAsync(SubscriptionDto subscriptionDto)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(subscriptionDto.Id);
        if (subscription == null)
        {
            throw new KeyNotFoundException($"Subskrypcja o identyfikatorze {subscriptionDto.Id} nie została znaleziona.");
        }

        subscription.UpdateSubscription(
            subscriptionDto.Name,
            subscriptionDto.DateFrom,
            subscriptionDto.DateTo,
            subscriptionDto.Price,
            subscriptionDto.AvatarPath);

        await _subscriptionRepository.UpdateAsync(subscription);
    }

    public async Task DeleteSubscriptionAsync(Guid id)
    {
        await _subscriptionRepository.DeleteAsync(id);
    }

    private SubscriptionStatusDto MapDomainStatusToDto(SubscriptionStatus domainStatus)
    {
        return domainStatus switch
        {
            SubscriptionStatus.Active => SubscriptionStatusDto.Active,
            SubscriptionStatus.Expiring => SubscriptionStatusDto.Expiring,
            SubscriptionStatus.Inactive => SubscriptionStatusDto.Inactive,
            _ => SubscriptionStatusDto.Inactive
        };
    }
}