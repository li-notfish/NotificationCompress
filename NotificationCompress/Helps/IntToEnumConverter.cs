using System.Globalization;

namespace NotificationCompress.Helps
{
    public class IntToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter != null && parameter.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(true) ? parameter : BindableProperty.UnsetValue;
        }
    }
}
