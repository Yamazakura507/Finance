using System.Globalization;

namespace Finance.Classes.Converters
{
    public class BooleanInvent : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("BooleanInvent можно использовать только в односторонем режиме(OneWay).");
        }
    }
}
