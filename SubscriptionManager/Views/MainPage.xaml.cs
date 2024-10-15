using SubscriptionManager.ViewModels;

namespace SubscriptionManager.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
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