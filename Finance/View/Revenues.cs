using Finance.Classes;
using Finance.Models;

namespace Finance.View
{
    public class Revenues : Abstract.AbstractViewModel
    {
        private int idDate;

        public decimal Sum
        {
            get;
            set;
        }
        public decimal SumStability
        {
            get;
            set;
        }
        public decimal SumNotStability
        {
            get;
            set;
        }
        public decimal SumActive
        {
            get;
            set;
        }
        public decimal SumPasive
        {
            get;
            set;
        }
        public decimal SumActiveStability
        {
            get;
            set;
        }
        public decimal SumPasiveStability
        {
            get;
            set;
        }
        public decimal SumNotActiveStability
        {
            get;
            set;
        }
        public decimal SumNotPasiveStability
        {
            get;
            set;
        }
        public bool IsRevenues
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
