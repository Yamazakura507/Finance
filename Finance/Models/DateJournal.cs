
namespace Finance.Models
{
    public class DateJournal : Abstract.AbstractModel<DateJournal>
    {
        private int year;
        private int month;
        private bool isCreateData;

        public int Year
        {
            get => !IsGet ? GetParametrs<int>("Year", this.GetType()) : year;
            set
            {   
                if (year != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<DateJournal>("Year", value);
                    }
                    year = value;
                }
            }
        }

        public int Month
        {
            get => !IsGet ? GetParametrs<int>("Month", this.GetType()) : month;
            set
            {
                if (month != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<DateJournal>("Month", value);
                    }
                    month = value;
                }
            }
        }

        public bool IsCreateData
        {
            get => !IsGet ? GetParametrs<bool>("IsCreateData", this.GetType()) : isCreateData;
            set
            {
                if (isCreateData != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<DateJournal>("IsCreateData", value);
                    }
                    isCreateData = value;
                }
            }
        }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
