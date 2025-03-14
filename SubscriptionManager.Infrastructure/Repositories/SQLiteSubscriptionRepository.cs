using Microsoft.EntityFrameworkCore;
using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Domain.Interfaces;
using SubscriptionManager.Infrastructure.Data;
using SubscriptionManager.Infrastructure.Mappers;

namespace SubscriptionManager.Infrastructure.Repositories;

public class SQLiteSubscriptionRepository : ISubscriptionRepository
{
    private readonly SubscriptionDbContext _dbContext;

    public SQLiteSubscriptionRepository(SubscriptionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        var entities = await _dbContext.Subscriptions.ToListAsync();
        return entities.Select(SubscriptionMapper.ToDomain);
    }

    public async Task<Subscription> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Subscriptions.FindAsync(id);
        return entity != null ? SubscriptionMapper.ToDomain(entity) : null;
    }

    public async Task AddAsync(Subscription subscription)
    {
        var entity = SubscriptionMapper.ToEntity(subscription);
        await _dbContext.Subscriptions.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        var entity = SubscriptionMapper.ToEntity(subscription);

        var existingEntity = await _dbContext.Subscriptions.FindAsync(entity.Id);
        if (existingEntity == null)
        {
            throw new KeyNotFoundException($"Subskrypcja o identyfikatorze {entity.Id} nie została znaleziona.");
        }

        _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Subscriptions.FindAsync(id);
        if (entity != null)
        {
            _dbContext.Subscriptions.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Subskrypcja o identyfikatorze {id} nie została znaleziona.");
        }
    }
}