using SubscriptionManager.UI.ViewModels;

namespace SubscriptionManager.UI.Views;

public partial class SubscriptionEditPage : ContentPage
{
    public SubscriptionEditPage(SubscriptionEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}