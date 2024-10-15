using SubscriptionManager.Views;

namespace SubscriptionManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SubscriptionDetailPage), typeof(SubscriptionDetailPage));
        }
    }
}
