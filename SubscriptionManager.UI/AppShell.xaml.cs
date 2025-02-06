using SubscriptionManager.UI.Views;

namespace SubscriptionManager.UI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("edit", typeof(SubscriptionEditPage));
    }
}
