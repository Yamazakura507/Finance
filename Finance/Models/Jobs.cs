using Finance.Classes;

namespace Finance.Models
{
    public class Jobs : Abstract.AbstractModelStatus<Jobs>
    {
        private decimal sumResult;
        private bool isDone;

        public decimal SumResult
        {
            get => !IsGet ? GetParametrs<decimal>("SumResult", this.GetType()) : sumResult;
            set
            {
                if (sumResult != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Jobs>("SumResult", value);
                    }
                    sumResult = value;
                }
            }
        }
        public bool IsDone
        {
            get => !IsGet ? GetParametrs<bool>("IsDone", this.GetType()) : isDone;
            set
            {
                if (isDone != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Jobs>("IsDone", value);
                    }
                    isDone = value;
                }
            }
        }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
