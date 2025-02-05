using SubscriptionManager.UI.ViewModels;

namespace SubscriptionManager.UI.Views;

public partial class SubscriptionListPage : ContentPage
{
    public SubscriptionListPage(SubscriptionListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}