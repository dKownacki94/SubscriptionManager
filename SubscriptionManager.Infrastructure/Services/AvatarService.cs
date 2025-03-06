using SubscriptionManager.Application.Interfaces;

namespace SubscriptionManager.Infrastructure.Services;

public class AvatarService : IAvatarService
{
    public IEnumerable<string> GetAvailableAvatars()
    {
        return new List<string>
        {
            "logo_netflix.png",
            "logo_prime.png",
            "logo_xbox.png",
            "logo_upload.png"};
    }
}