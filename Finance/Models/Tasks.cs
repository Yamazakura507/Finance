

namespace Finance.Models
{
    public class Tasks : Abstract.AbstractModelStatus<Tasks>
    {
        private decimal desiredResult;

        public decimal DesiredResult
        {
            get => !IsGet ? GetParametrs<decimal>("DesiredResult", this.GetType()) : desiredResult;
            set
            {
                if (desiredResult != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Tasks>("DesiredResult", value);
                    }
                    desiredResult = value;
                }
            }
        }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
