using Finance.Classes;
using Finance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.View
{
    public class Result
    {
        private int idDate;

        public int Id
        {
            get;
            set;
        }
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
                var date = DBModel.GetModel<DateJournal>(value);
                DateStr = $"{date.Year} {new DateTime(date.Year, date.Month, 1).ToString("MMMM")}";
                idDate = value;
            }
        }

        public string DateStr { get; set; }
    }
}
