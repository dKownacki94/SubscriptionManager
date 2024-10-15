using SubscriptionManager.ViewModels;

namespace SubscriptionManager.Views;

[QueryProperty(nameof(SubscriptionId), "Id")]
public partial class SubscriptionDetailPage : ContentPage
{
    public int SubscriptionId { get; set; }

    public SubscriptionDetailPage(SubscriptionDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SubscriptionDetailViewModel viewModel)
        {
            viewModel.SubscriptionId = SubscriptionId;
            await viewModel.LoadSubscriptionAsync();
        }
    }
}