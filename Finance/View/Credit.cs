
using Finance.Classes;

namespace Finance.View
{
    public class Credit : Abstract.AbstractViewStatus<Credit>
    {
        private int idStatusCredit {  get; set; }

        public decimal Sum
        {
            get;
            set;
        }
        public decimal Percent
        {
            get;
            set;
        }
        public decimal OverpaymentSumReal
        {
            get;
            set;
        }
        public int IdStatusCredit
        {
            get => idStatusCredit;
            set
            {
                CreditStatus = GetModel<Models.CreditStatus>(value);
                idStatusCredit = value;
            }
        }
        public decimal MonthSum
        {
            get;
            set;
        }
        public DateTime RealEndDate
        {
            get;
            set;
        }

        public Models.CreditStatus CreditStatus
        {
            get; set;
        }

        private new string Description { get; set; }
    }
}
