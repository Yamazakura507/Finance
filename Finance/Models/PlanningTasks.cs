

namespace Finance.Models
{
    public class PlanningTasks : Abstract.AbstractModel<PlanningTasks>
    {
        private int idTask;
        private int idPlan;

        public int IdTask
        {
            get => !IsGet ? GetParametrs<int>("IdTask", this.GetType()) : idTask ;
            set
            {
                if (idTask != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<PlanningTasks>("IdTask", value);
                    }

                    Task = GetModel<View.Tasks>(value);
                    idTask = value;
                }
            }
        }

        public int IdPlan
        {
            get => !IsGet ? GetParametrs<int>("IdPlan", this.GetType()) : idPlan;
            set
            {
                if (idPlan != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<PlanningTasks>("IdPlan", value);
                    }

                    Plan = GetModel<View.PlanningJournal>(value);
                    idPlan = value;
                }
            }
        }

        public View.PlanningJournal Plan { get; private set; }
        public View.Tasks Task { get; private set; }
    }
}
