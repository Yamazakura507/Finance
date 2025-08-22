using Finance.Classes;

namespace Finance.View
{
    public class Jobs : Abstract.AbstractViewStatus<Jobs>
    {
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
