using AutoMapper;
using SubscriptionManager.Domain.Entities;
using SubscriptionManager.Infrastructure.DataModels;

namespace SubscriptionManager.Infrastructure.Mapping;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<Subscription, SubscriptionEntity>();

        CreateMap<SubscriptionEntity, Subscription>()
            .ConstructUsing(src => new Subscription(
                src.Name,
                src.DateFrom,
                src.DateTo,
                src.Price,
                src.AvatarPath))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}