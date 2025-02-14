using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubscriptionManager.Application.DTOs;
using SubscriptionManager.Application.Interfaces;

namespace SubscriptionManager.UI.ViewModels;

[QueryProperty(nameof(SubscriptionId), "id")]
public partial class SubscriptionEditViewModel : ObservableObject
{
    private readonly ISubscriptionService _subscriptionService;

    private SubscriptionDto _subscription;

    [ObservableProperty]
    private Guid _subscriptionId;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private decimal _price;

    [ObservableProperty]
    private DateTime _startDate = DateTime.Today;

    [ObservableProperty]
    private DateTime _endDate = DateTime.Today.AddMonths(1);

    [ObservableProperty]
    private string _avatarPath = "dotnet_bot.png";

    [ObservableProperty]
    private string pageTitle = "Dodaj Subskrypcję";

    public SubscriptionEditViewModel(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
        _subscription = new SubscriptionDto()
        {
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today.AddMonths(1)
        };
    }

    [RelayCommand]
    private void Appearing()
    {
        LoadSubscriptionCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadSubscriptionAsync()
    {
        if (SubscriptionId != Guid.Empty)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(SubscriptionId);
            if (subscription != null)
            {
                _subscription = subscription;
                MapSubscriptionToProperties(_subscription);
                PageTitle = "Edytuj Subskrypcję";
                return;
            }
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            MapPropertiesToSubscription(_subscription);

            if (SubscriptionId == Guid.Empty)
            {
                await _subscriptionService.AddSubscriptionAsync(_subscription);
            }
            else
            {
                await _subscriptionService.UpdateSubscriptionAsync(_subscription);
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Błąd", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task DeleteAsync() 
    {
        if (SubscriptionId == Guid.Empty)
            return;

        try
        {
            await _subscriptionService.DeleteSubscriptionAsync(SubscriptionId);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Błąd", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task PickAvatarAsync()
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Wybierz avatar",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                var newFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);

                using (var stream = await result.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFilePath))
                {
                    await stream.CopyToAsync(newStream);
                }

                AvatarPath = newFilePath;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Błąd", ex.Message, "OK");
        }
    }

    private void MapSubscriptionToProperties(SubscriptionDto subscription)
    {
        Name = subscription.Name;
        Price = subscription.Price;
        StartDate = subscription.DateFrom;
        EndDate = subscription.DateTo;
        AvatarPath = subscription.AvatarPath;
    }

    private void MapPropertiesToSubscription(SubscriptionDto subscription)
    {
        subscription.Name = Name;
        subscription.Price = Price;
        subscription.DateFrom = StartDate;
        subscription.DateTo = EndDate;
        subscription.AvatarPath = AvatarPath;
    }
}