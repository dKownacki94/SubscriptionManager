using Plugin.LocalNotification;
using SubscriptionManager.Models;

namespace SubscriptionManager.Services
{
    public class NotificationService
    {
        public async Task ScheduleNotificationsAsync(Subscription subscription)
        {
            // Anuluj istniejące powiadomienia dla tej subskrypcji
            LocalNotificationCenter.Current.Cancel(subscription.Id);

            // Powiadomienie na 3 dni przed
            if (subscription.EndDate.AddDays(-3) > DateTime.Now)
            {
                var notification3Days = new NotificationRequest
                {
                    NotificationId = subscription.Id * 10 + 1,
                    Title = "Subskrypcja wygasa wkrótce",
                    Description = $"Twoja subskrypcja {subscription.Name} wygasa za 3 dni.",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = subscription.EndDate.AddDays(-3)
                    }
                };
                await LocalNotificationCenter.Current.Show(notification3Days);
            }

            // Powiadomienie na 1 dzień przed
            if (subscription.EndDate.AddDays(-1) > DateTime.Now)
            {
                var notification1Day = new NotificationRequest
                {
                    NotificationId = subscription.Id * 10 + 2,
                    Title = "Subskrypcja wygasa jutro",
                    Description = $"Twoja subskrypcja {subscription.Name} wygasa jutro.",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = subscription.EndDate.AddDays(-1)
                    }
                };
                await LocalNotificationCenter.Current.Show(notification1Day);
            }
        }
    }
}
