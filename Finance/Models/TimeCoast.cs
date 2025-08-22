
namespace Finance.Models
{
    public class TimeCoast : Abstract.AbstractModelStatus<TimeCoast>
    {
        private int minute;
        public int Minute
        {
            get => !IsGet ? GetParametrs<int>("Minute", this.GetType()) : minute;
            set
            {
                if (minute != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<TimeCoast>("Minute", value);
                    }
                    minute = value;
                }
            }
        }
    }
}
