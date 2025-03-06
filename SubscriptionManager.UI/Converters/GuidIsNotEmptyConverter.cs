using System.Globalization;

namespace SubscriptionManager.UI.Converters;

public class GuidIsNotEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Guid guid)
        {
            return guid != Guid.Empty;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}