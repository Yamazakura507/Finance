using System.Globalization;

namespace Finance.Classes.Converters
{
    public class IsNumberCompressionConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            double val1 = System.Convert.ToDouble(value[0]);
            double val2 = System.Convert.ToDouble(value[1]);
            bool? is_flag = null;

            if (value.Count() > 5)
            {
                is_flag = System.Convert.ToInt32(value[2]) == 1;
            }

            if (val1 == val2) return is_flag is null ? Color.FromHex(value[3].ToString()) : Color.FromHex(value[4].ToString());

            if (is_flag is null)
            {
                return val1 > val2 ? Color.FromHex(value[2].ToString()) : Color.FromHex(value[4].ToString());
            }
            else
            {
                return (is_flag.Value ? val1 > val2 : val1 < val2) ? Color.FromHex(value[3].ToString()) : Color.FromHex(value[5].ToString());
            }
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNumberCompressionConverter можно использовать только в односторонем режиме(OneWay).");
        }
    }
}
