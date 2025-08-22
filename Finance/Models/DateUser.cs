

namespace Finance.Models
{
    public class DateUser : Abstract.AbstractModel<DateUser>
    {
        private int idDate;

        public int IdDate
        {
            get => !IsGet ? GetParametrs<int>("IdDate", this.GetType()) : idDate ;
            set
            {
                if (idDate != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<DateUser>("IdDate", value);
                    }

                    DateJournal = GetModel<DateJournal>(value);
                    idDate = value;
                }
            }
        }

        public DateJournal DateJournal { get; private set; }
    }
}
