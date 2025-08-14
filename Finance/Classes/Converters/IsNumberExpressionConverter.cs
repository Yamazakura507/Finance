using System.Globalization;

namespace Finance.Classes.Converters
{
    public class IsNumberExpressionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = System.Convert.ToDouble(value);
            double par = System.Convert.ToDouble(parameter.ToString().Replace("!",String.Empty));

            return parameter.ToString().Contains('!') ? val != par : val == par;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNumberExpressionConverter можно использовать только в односторонем режиме(OneWay).");
        }
    }
}
