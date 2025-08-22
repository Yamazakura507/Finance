using Finance.Classes;
using Finance.Models;

namespace Finance.View
{
    public class Result : Abstract.AbstractViewModel
    {
        private int idDate;

        public decimal Profit
        {
            get;
            set;
        }
        public decimal ProfitForEvrifing
        {
            get;
            set;
        }
        public decimal DebtorSum
        {
            get;
            set;
        }

        public int IdDate
        {
            get => idDate;
            set
            {
                var date = GetModel<DateJournal>(value);
                DateStr = $"{date.Year} {new DateTime(date.Year, date.Month, 1).ToString("MMMM")}";
                idDate = value;
            }
        }

        public string DateStr { get; set; }
    }
}
