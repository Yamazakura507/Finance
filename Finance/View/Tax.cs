using Finance.Classes;

namespace Finance.View
{
    public class Tax : DBModel
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

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
    }
}
