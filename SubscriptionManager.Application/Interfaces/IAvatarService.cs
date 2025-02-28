namespace SubscriptionManager.Application.Interfaces;

public interface IAvatarService
{
    IEnumerable<string> GetAvailableAvatars();
}