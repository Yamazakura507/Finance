using Finance.Classes;

namespace Finance.View
{
    public class PlanningJournal : Abstract.AbstractViewStatus<PlanningJournal>
    {
        private int id;

        public new int Id
        {
            get => id;
            set
            {
                CountTask = Convert.ToInt32(ResultRequest($"SELECT count(*) FROM `PlanningTasks` pt WHERE pt.`IdPlan` = '{value}'"));
                id = value;
            }
        }

        public decimal TargetAmount
        {
            get;
            set;
        }

        public int CountTask { get; set; }
    }
}
