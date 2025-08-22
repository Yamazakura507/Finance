using Finance.Classes;

namespace Finance.View
{
    public class Scalping : Abstract.AbstractViewStatus<Scalping>
    {
        private int id;
        private decimal margin;
        private decimal marginBeforeTax;
        private decimal commission;

        public new int Id
        {
            get => id;
            set
            {
                CountOrder = Convert.ToInt32(ResultRequest($"SELECT count(*) FROM `ScalpingEntries` se WHERE se.`IdScalping` = '{value}'"));
                id = value;
            }
        }

        public decimal Margin
        {
            get => margin;
            set
            {
                IsProfit = value >= 0;
                margin = value;
            }
        }

        public decimal MarginBeforeTax
        {
            get => marginBeforeTax;
            set
            {
                IsProfitBeforeTax = value >= 0;
                marginBeforeTax = value;
            }
        }

        public decimal Commissing
        {
            get => commission;
            set
            {
                IsCommission = value > 0;
                commission = value;
            }
        }

        public int CountOrder { get; set; }

        public bool IsProfit { get; set; }
        public bool IsProfitBeforeTax { get; set; }
        public bool IsCommission { get; set; }
    }
}
