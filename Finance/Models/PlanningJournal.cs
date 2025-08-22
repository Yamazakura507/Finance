using Finance.Classes;

namespace Finance.Models
{
    public class PlanningJournal : Abstract.AbstractModelStatus<PlanningJournal>
    {
        private decimal targetAmount;

        public decimal TargetAmount
        {
            get => !IsGet ? GetParametrs<decimal>("TargetAmount", this.GetType()) : targetAmount;
            set
            {
                if (targetAmount != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<PlanningJournal>("TargetAmount", value);
                    }
                    targetAmount = value;
                }
            }
        }
    }
}
