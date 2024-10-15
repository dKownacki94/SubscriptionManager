using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using SubscriptionManager.Models;
using SubscriptionManager.Services;
using SubscriptionManager.Views;

namespace SubscriptionManager.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Subscription> Subscriptions { get; } = new();

        public IAsyncRelayCommand LoadSubscriptionsCommand { get; }
        public IAsyncRelayCommand AddSubscriptionCommand { get; }
        public IAsyncRelayCommand<Subscription> SubscriptionTappedCommand { get; }

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            LoadSubscriptionsCommand = new AsyncRelayCommand(LoadSubscriptionsAsync);
            AddSubscriptionCommand = new AsyncRelayCommand(OnAddSubscriptionAsync);
            SubscriptionTappedCommand = new AsyncRelayCommand<Subscription>(OnSubscriptionTappedAsync);
        }

        private async Task LoadSubscriptionsAsync()
        {
            Subscriptions.Clear();
            var subscriptions = await _databaseService.GetSubscriptionsAsync();
            foreach (var sub in subscriptions)
            {
                Subscriptions.Add(sub);
            }
        }

        private async Task OnAddSubscriptionAsync()
        {
           await Shell.Current.GoToAsync(nameof(SubscriptionDetailPage));
        }

        private async Task OnSubscriptionTappedAsync(Subscription subscription)
        {
            await Shell.Current.GoToAsync($"{nameof(SubscriptionDetailPage)}?Id={subscription.Id}");
        }
    }
}
