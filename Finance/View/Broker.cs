using Finance.Classes;

namespace Finance.View
{
    public class Broker : DBModel
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

        public decimal Commissing
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }
}
