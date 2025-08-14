using System.Globalization;

namespace Finance.Classes.Converters
{
    public class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isNull = value == null || value == DBNull.Value;

            return parameter is null ? isNull : !isNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNullConverter можно использовать только в односторонем режиме(OneWay).");
        }
    }
}
