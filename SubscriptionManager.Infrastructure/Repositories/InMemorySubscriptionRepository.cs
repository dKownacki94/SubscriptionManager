using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Domain.Interfaces;

namespace SubscriptionManager.Infrastructure.Repositories;

public class InMemorySubscriptionRepository : ISubscriptionRepository
{
    private readonly List<Subscription> _subscriptions = new();

    public Task<IEnumerable<Subscription>> GetAllAsync()
    {

        _subscriptions.Add(new Subscription("Prime Video", DateTime.Now, DateTime.Now.AddDays(1), 50, "dotnet_bot.png"));

        return Task.FromResult(_subscriptions.AsEnumerable());
    }

    public Task<Subscription> GetByIdAsync(Guid id)
    {
        var subscription = _subscriptions.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(subscription);
    }

    public Task AddAsync(Subscription subscription)
    {
        if (subscription == null)
            throw new ArgumentNullException(nameof(subscription));

        _subscriptions.Add(subscription);
        return Task.CompletedTask;
    }

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