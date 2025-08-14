using System.Globalization;

namespace Finance.Classes.Converters
{
    public class IsNumberCommpressionZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = System.Convert.ToDouble(value);

            return val > 0 ? 1 : val == 0 ? 0 : -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToBoolean(parameter))
            {
                return Convert(value, targetType, parameter, culture);
            }
            else
            {
                throw new InvalidOperationException("IsNumberCommpressionZeroConverter можно использовать только в односторонем режиме(OneWay).");
            }
        }
    }
}
