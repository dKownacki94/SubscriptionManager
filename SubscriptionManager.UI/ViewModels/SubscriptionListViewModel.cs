using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubscriptionManager.Application.DTOs;
using SubscriptionManager.Application.Interfaces;
using System.Collections.ObjectModel;

namespace SubscriptionManager.UI.ViewModels;

public partial class SubscriptionListViewModel : ObservableObject
{
    private readonly ISubscriptionService _subscriptionService;

    [ObservableProperty]
    private SubscriptionDto? _selectedSubscription;

    [ObservableProperty]
    public ObservableCollection<SubscriptionDto> subscriptions;

    public SubscriptionListViewModel(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
        Subscriptions = new ObservableCollection<SubscriptionDto>();
    }

    [RelayCommand]
    void Appearing()
    {
        LoadSubscriptionsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadSubscriptions()
    {
        var subs = await _subscriptionService.GetAllSubscriptionsAsync();
        Subscriptions.Clear();
        foreach (var sub in subs)
        {
            Subscriptions.Add(sub);
        }
    }

    [RelayCommand]
    private async Task AddSubscription()
    {
        await Shell.Current.GoToAsync("edit");
    }

    [RelayCommand]
    private async Task SelectedSubscriptionChanged(SubscriptionDto selectedSubscription)
    {
        if (selectedSubscription != null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "id",selectedSubscription.Id}
                };

            await Shell.Current.GoToAsync("edit", parameters);
            SelectedSubscription = null;
        }
    }
}