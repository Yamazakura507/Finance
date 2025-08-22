

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

                    Task = GetModel<Tasks>(value);
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

                    Plan = GetModel<PlanningJournal>(value);
                    idPlan = value;
                }
            }
        }

        public PlanningJournal Plan { get; private set; }
        public Tasks Task { get; private set; }
    }
}
