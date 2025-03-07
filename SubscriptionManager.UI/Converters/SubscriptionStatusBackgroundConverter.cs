using SubscriptionManager.Application.DTOs;
using System.Globalization;

namespace SubscriptionManager.UI.Converters;

public class SubscriptionStatusBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SubscriptionStatusDto status)
        {
            return status switch
            {
                SubscriptionStatusDto.Active => Microsoft.Maui.Controls.Application.Current.Resources["SuccessLight"],
                SubscriptionStatusDto.Expiring => Microsoft.Maui.Controls.Application.Current.Resources["WarningLight"],
                SubscriptionStatusDto.Inactive => Microsoft.Maui.Controls.Application.Current.Resources["DangerLight"],
                _ => Microsoft.Maui.Controls.Application.Current.Resources["Gray200"]
            };
        }

        return Microsoft.Maui.Controls.Application.Current.Resources["Gray200"];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
