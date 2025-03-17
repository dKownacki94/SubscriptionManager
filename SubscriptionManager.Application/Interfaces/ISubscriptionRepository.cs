using SubscriptionManager.Domain.Entities;

namespace SubscriptionManager.Application.Interfaces;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetAllAsync();
    Task<Subscription> GetByIdAsync(Guid id);
    Task AddAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
    Task DeleteAsync(Guid id);
}
