using SubscriptionManager.Application.DTOs;
using SubscriptionManager.Application.Interfaces;
using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Domain.Interfaces;

namespace SubscriptionManager.Application.Services;

/// <summary>
/// Implementacja logiki przypadków użycia związanej z subskrypcjami.
/// </summary>
public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    /// <summary>
    /// Pobiera wszystkie subskrypcje i mapuje je do DTO.
    /// </summary>
    public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.GetAllAsync();
        return subscriptions.Select(s => new SubscriptionDto
        {
            Id = s.Id,
            Name = s.Name,
            DateFrom = s.DateFrom,
            DateTo = s.DateTo,
            Price = s.Price,
            AvatarPath = s.AvatarPath
        });
    }

    /// <summary>
    /// Pobiera subskrypcję o podanym identyfikatorze i mapuje ją do DTO.
    /// </summary>
    public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(id);
        if (subscription == null)
            return null;

        return new SubscriptionDto
        {
            Id = subscription.Id,
            Name = subscription.Name,
            DateFrom = subscription.DateFrom,
            DateTo = subscription.DateTo,
            Price = subscription.Price,
            AvatarPath = subscription.AvatarPath
        };
    }

    /// <summary>
    /// Dodaje nową subskrypcję.
    /// Tworzy obiekt domenowy na podstawie DTO i zapisuje go przy użyciu repozytorium.
    /// </summary>
    public async Task AddSubscriptionAsync(SubscriptionDto subscriptionDto)
    {
        // Tworzymy obiekt domenowy – walidacja odbywa się w konstruktorze encji.
        var subscription = new Subscription(
            subscriptionDto.Name,
            subscriptionDto.DateFrom,
            subscriptionDto.DateTo,
            subscriptionDto.Price,
            subscriptionDto.AvatarPath);

        await _subscriptionRepository.AddAsync(subscription);
    }

    /// <summary>
    /// Aktualizuje istniejącą subskrypcję.
    /// Pobiera aktualny obiekt domenowy, aktualizuje go i zapisuje zmiany.
    /// </summary>
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

    /// <summary>
    /// Usuwa subskrypcję o podanym identyfikatorze.
    /// </summary>
    public async Task DeleteSubscriptionAsync(Guid id)
    {
        await _subscriptionRepository.DeleteAsync(id);
    }
}