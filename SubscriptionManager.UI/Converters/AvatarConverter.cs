using System.Globalization;

namespace SubscriptionManager.UI.Converters;

public class AvatarConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string path = value as string;
        if (string.IsNullOrEmpty(path))
        {
            return "logo_netflix.png";
        }
        return path;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
