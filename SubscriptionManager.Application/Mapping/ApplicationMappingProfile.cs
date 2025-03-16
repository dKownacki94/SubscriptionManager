using AutoMapper;
using SubscriptionManager.Application.DTOs;
using SubscriptionManager.Domain.Entities;

namespace SubscriptionManager.Application.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<Subscription, SubscriptionDto>()
            .ForMember(dest => dest.Status, opt =>
                opt.MapFrom((src, _, _, context) =>
                    MapStatus(src.GetStatus(DateTime.Today))));

        CreateMap<SubscriptionDto, Subscription>()
            .ConstructUsing(src => new Subscription(
                src.Name,
                src.DateFrom,
                src.DateTo,
                src.Price,
                src.AvatarPath))
            .ForMember(dest => dest.Id, opt =>
                opt.Condition(src => src.Id != Guid.Empty))
            .AfterMap((src, dest) => {
                if (src.Id != Guid.Empty)
                {
                    typeof(Subscription).GetProperty("Id")
                        .SetValue(dest, src.Id);
                }
            });
    }

    private static SubscriptionStatusDto MapStatus(SubscriptionStatus status)
    {
        return status switch
        {
            SubscriptionStatus.Active => SubscriptionStatusDto.Active,
            SubscriptionStatus.Expiring => SubscriptionStatusDto.Expiring,
            SubscriptionStatus.Inactive => SubscriptionStatusDto.Inactive,
            _ => SubscriptionStatusDto.Inactive
        };
    }
}