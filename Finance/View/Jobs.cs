using Finance.Classes;

namespace Finance.View
{
    public class Jobs : DBModel
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

        public string Commit
        {
            get;
            set;
        }

        public decimal SumResult
        {
            get;
            set;
        }
        public bool IsDone
        {
            get;
            set;
        }
    }
}
