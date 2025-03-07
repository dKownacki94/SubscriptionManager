using SubscriptionManager.Application.DTOs;
using System.Globalization;

namespace SubscriptionManager.UI.Converters;

public class SubscriptionStatusColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SubscriptionStatusDto status)
        {
            return status switch
            {
                SubscriptionStatusDto.Active => Microsoft.Maui.Controls.Application.Current.Resources["Success"],
                SubscriptionStatusDto.Expiring => Microsoft.Maui.Controls.Application.Current.Resources["Warning"],
                SubscriptionStatusDto.Inactive => Microsoft.Maui.Controls.Application.Current.Resources["Danger"],
                _ => Microsoft.Maui.Controls.Application.Current.Resources["TextTertiary"]
            };
        }

        return Microsoft.Maui.Controls.Application.Current.Resources["TextTertiary"];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
