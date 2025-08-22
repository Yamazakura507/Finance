using Finance.Classes;

namespace Finance.View
{
    public class Tax : Abstract.AbstractViewStatus<Tax>
    {

        public decimal TaxPercent
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        private new string Description { get; set; }
    }
}
