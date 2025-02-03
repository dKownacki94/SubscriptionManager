using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Domain.Interfaces;

namespace SubscriptionManager.Infrastructure.Repositories;

/// <summary>
/// Przykładowa implementacja repozytorium subskrypcji przechowywanego w pamięci.
/// </summary>
public class InMemorySubscriptionRepository : ISubscriptionRepository
{
    // Prywatna kolekcja symulująca bazę danych.
    private readonly List<Subscription> _subscriptions = new();

    /// <summary>
    /// Pobiera wszystkie subskrypcje.
    /// </summary>
    public Task<IEnumerable<Subscription>> GetAllAsync()
    {
        // Zwracamy kopię kolekcji jako IEnumerable.
        return Task.FromResult(_subscriptions.AsEnumerable());
    }

    /// <summary>
    /// Pobiera subskrypcję na podstawie identyfikatora.
    /// </summary>
    public Task<Subscription> GetByIdAsync(Guid id)
    {
        var subscription = _subscriptions.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(subscription);
    }

    /// <summary>
    /// Dodaje nową subskrypcję.
    /// </summary>
    public Task AddAsync(Subscription subscription)
    {
        if (subscription == null)
            throw new ArgumentNullException(nameof(subscription));

        _subscriptions.Add(subscription);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Aktualizuje istniejącą subskrypcję.
    /// </summary>
    public Task UpdateAsync(Subscription subscription)
    {
        if (subscription == null)
            throw new ArgumentNullException(nameof(subscription));

        var index = _subscriptions.FindIndex(s => s.Id == subscription.Id);
        if (index != -1)
        {
            _subscriptions[index] = subscription;
        }
        else
        {
            throw new KeyNotFoundException($"Subskrypcja o identyfikatorze {subscription.Id} nie została znaleziona.");
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Usuwa subskrypcję o podanym identyfikatorze.
    /// </summary>
    public Task DeleteAsync(Guid id)
    {
        var subscription = _subscriptions.FirstOrDefault(s => s.Id == id);
        if (subscription != null)
        {
            _subscriptions.Remove(subscription);
        }
        else
        {
            throw new KeyNotFoundException($"Subskrypcja o identyfikatorze {id} nie została znaleziona.");
        }

        return Task.CompletedTask;
    }
}