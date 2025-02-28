using SubscriptionManager.Application.Interfaces;

namespace SubscriptionManager.Infrastructure.Services;

public class AvatarService : IAvatarService
{
    public IEnumerable<string> GetAvailableAvatars()
    {
        return new List<string>
        {
            "logo_netflix.png",
            "dotnet_bot.png"        };
    }
}