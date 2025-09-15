using YahooFinanceApi;
using System.Linq;

namespace Finance.Classes.Converters
{
    public static class ConvertYahooFinancePrice
    {
        async static public Task<object> ConvertAsync(object value)
        {
            try
            {
                string ticker = value.ToString();

                var securities = await Yahoo.Symbols(ticker).Fields(Field.Symbol, Field.RegularMarketPrice).QueryAsync();

                var active = securities[ticker];

                return "\"" + ticker + "\" - " + active[Field.RegularMarketPrice];
            }
            catch (Exception)
            {
                return "Тикер отсутствует на Yahoo Finance";
            }
        }
    }
}
