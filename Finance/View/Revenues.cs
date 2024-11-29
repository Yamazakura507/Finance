using Finance.Classes;
using Finance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.View
{
    public class Revenues
    {
        private int idDate;

        public int Id
        {
            get;
            set;
        }
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
                var date = DBModel.GetModel<DateJournal>(value);
                DateStr = $"{date.Year} {new DateTime(date.Year, date.Month, 1).ToString("MMMM")}";
                idDate = value;
            }
        }

        public string DateStr { get; set; }
    }
}
