using System.Globalization;

namespace Finance.Classes.Converters
{
    public class SumConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal sum = 0;

            for (int i = 0; i < value.Length; i++)
            {
                sum += System.Convert.ToDecimal(value[i]);
            }

            return sum;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("SumConverter можно использовать только в односторонем режиме(OneWay).");
        }
    }
}
