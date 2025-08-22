using Finance.Classes;

namespace Finance.Models
{
    public class Tax : Abstract.AbstractModelStatus<Tax>
    {
        private decimal taxPercent;
        private DateTime date;

        public decimal TaxPercent
        {
            get => !IsGet ? GetParametrs<decimal>("TaxPercent", this.GetType()) : taxPercent;
            set
            {
                if (taxPercent != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Tax>("TaxPercent", value);
                    }
                    taxPercent = value;
                }
            }
        }
        public DateTime Date
        {
            get => !IsGet ? GetParametrs<DateTime>("Date", this.GetType()) : date;
            set
            {
                if (date != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Tax>("Date", value);
                    }

                    date = value;
                }
            }
        }

        private new string Description { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
