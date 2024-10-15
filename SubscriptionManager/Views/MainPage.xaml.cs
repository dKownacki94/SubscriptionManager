using SubscriptionManager.ViewModels;

namespace SubscriptionManager.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainViewModel viewModel)
        {
            await viewModel.LoadSubscriptionsCommand.ExecuteAsync(null);
        }
    }

    private async void OnSubscriptionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Subscription selectedSubscription)
        {
            await ((MainViewModel)BindingContext).SubscriptionTappedCommand.ExecuteAsync(selectedSubscription);
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}