using AutoMapper;
using SubscriptionManager.Application.DTOs;
using SubscriptionManager.Application.Interfaces;
using SubscriptionManager.Domain.Entities;

namespace SubscriptionManager.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMapper _mapper;

    public SubscriptionService(
        ISubscriptionRepository subscriptionRepository,
        IMapper mapper)
    {
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
    }

    public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(id);
        return _mapper.Map<SubscriptionDto>(subscription);
    }

    public async Task AddSubscriptionAsync(SubscriptionDto subscriptionDto)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionDto);
        await _subscriptionRepository.AddAsync(subscription);
    }

    public async Task UpdateSubscriptionAsync(SubscriptionDto subscriptionDto)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionDto);
        await _subscriptionRepository.UpdateAsync(subscription);
    }

    public async Task DeleteSubscriptionAsync(Guid id)
    {
        await _subscriptionRepository.DeleteAsync(id);
    }
}