using System.Globalization;

namespace Finance.Classes.Converters
{
    public class DivConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal sum = System.Convert.ToDecimal(value[0]);

            for (int i = 1; i < value.Length; i++)
            {
                sum -= System.Convert.ToDecimal(value[i]);
            }

            return sum;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("DivConverter можно использовать только в односторонем режиме(OneWay).");
        }
    }
}
