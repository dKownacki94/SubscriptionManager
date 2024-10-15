using CommunityToolkit.Mvvm.Input;
using SubscriptionManager.Models;
using SubscriptionManager.Services;

namespace SubscriptionManager.ViewModels
{
    [QueryProperty(nameof(SubscriptionId), "Id")]
    public partial class SubscriptionDetailViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private readonly NotificationService _notificationService;

        public int SubscriptionId { get; set; }

        private string name;
        private DateTime startDate;
        private DateTime endDate;
        private decimal price;
        private string avatar;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public DateTime StartDate { get => startDate; set => SetProperty(ref startDate, value); }
        public DateTime EndDate { get => endDate; set => SetProperty(ref endDate, value); }
        public decimal Price { get => price; set => SetProperty(ref price, value); }
        public string Avatar { get => avatar; set => SetProperty(ref avatar, value); }

        public IRelayCommand SaveCommand { get; }

        public SubscriptionDetailViewModel(DatabaseService databaseService, NotificationService notificationService)
        {
            _databaseService = databaseService;
            _notificationService = notificationService;

            SaveCommand = new AsyncRelayCommand(SaveAsync);
        }

        public async Task LoadSubscriptionAsync()
        {
            if (SubscriptionId != 0)
            {
                var subscription = await _databaseService.GetSubscriptionAsync(SubscriptionId);
                Name = subscription.Name;
                StartDate = subscription.StartDate;
                EndDate = subscription.EndDate;
                Price = subscription.Price;
                Avatar = subscription.Avatar;
            }
            else
            {
                StartDate = DateTime.Today;
                EndDate = DateTime.Today.AddMonths(1);
            }
        }

        private async Task SaveAsync()
        {
            var subscription = new Subscription
            {
                Id = SubscriptionId,
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate,
                Price = Price,
                Avatar = Avatar
            };

            await _databaseService.SaveSubscriptionAsync(subscription);

            // Zaplanuj powiadomienia
            await _notificationService.ScheduleNotificationsAsync(subscription);

            // Powrót do poprzedniej strony
            await Shell.Current.GoToAsync("..");
        }
    }
}
