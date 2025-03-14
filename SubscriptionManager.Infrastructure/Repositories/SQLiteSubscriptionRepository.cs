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
        var dtos = await _dbContext.Subscriptions.ToListAsync();
        return dtos.Select(SubscriptionMapper.ToDomain);
    }

    public async Task<Subscription> GetByIdAsync(Guid id)
    {
        var dto = await _dbContext.Subscriptions.FindAsync(id);
        return dto != null ? SubscriptionMapper.ToDomain(dto) : null;
    }

    public async Task AddAsync(Subscription subscription)
    {
        var dto = SubscriptionMapper.ToDto(subscription);
        await _dbContext.Subscriptions.AddAsync(dto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        var dto = SubscriptionMapper.ToDto(subscription);

        var existingDto = await _dbContext.Subscriptions.FindAsync(dto.Id);
        if (existingDto == null)
        {
            throw new KeyNotFoundException($"Subskrypcja o identyfikatorze {dto.Id} nie została znaleziona.");
        }

        _dbContext.Entry(existingDto).CurrentValues.SetValues(dto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var dto = await _dbContext.Subscriptions.FindAsync(id);
        if (dto != null)
        {
            _dbContext.Subscriptions.Remove(dto);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Subskrypcja o identyfikatorze {id} nie została znaleziona.");
        }
    }
}