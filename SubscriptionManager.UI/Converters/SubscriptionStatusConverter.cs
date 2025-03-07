using System.Globalization;
using SubscriptionManager.Application.DTOs;

namespace SubscriptionManager.UI.Converters;

public class SubscriptionStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SubscriptionStatusDto status)
        {
            return status switch
            {
                SubscriptionStatusDto.Active => "Aktywna",
                SubscriptionStatusDto.Expiring => "Wygasa",
                SubscriptionStatusDto.Inactive => "Nieaktywna",
                _ => "Nieznany"
            };
        }

        return "Nieznany";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}