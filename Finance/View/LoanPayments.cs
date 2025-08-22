
namespace Finance.View
{
    public class LoanPayments : Abstract.AbstractViewModel
    {
        public decimal Sum
        {
            get;
            set;
        }
        public bool IsBasic
        {
            get;
            set;
        }
        public bool? IsTerm
        {
            get;
            set;
        }
        public DateTime? DatePay
        {
            get;
            set;
        }
        public decimal? OverflowSum
        {
            get;
            set;
        }
        public decimal BeginSum
        {
            get;
            set;
        }
        public decimal RemnantSum
        {
            get;
            set;
        }
    }
}
