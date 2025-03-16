using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Domain.Interfaces;
using SubscriptionManager.Infrastructure.Data;
using SubscriptionManager.Infrastructure.DataModels;

namespace SubscriptionManager.Infrastructure.Repositories;

public class SQLiteSubscriptionRepository : ISubscriptionRepository
{
    private readonly SubscriptionDbContext _dbContext;
    private readonly IMapper _mapper;

    public SQLiteSubscriptionRepository(
        SubscriptionDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        var entities = await _dbContext.Subscriptions.ToListAsync();
        return _mapper.Map<IEnumerable<Subscription>>(entities);
    }

    public async Task<Subscription> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Subscriptions.FindAsync(id);
        return _mapper.Map<Subscription>(entity);
    }

    public async Task AddAsync(Subscription subscription)
    {
        var entity = _mapper.Map<SubscriptionEntity>(subscription);
        await _dbContext.Subscriptions.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        var entity = _mapper.Map<SubscriptionEntity>(subscription);

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