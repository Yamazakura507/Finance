using Finance.Classes;

namespace Finance.View
{
    public class PlanningJournal : DBModel
    {
        private int id;

        public int Id
        {
            get => id;
            set
            {
                CountTask = Convert.ToInt32(DBModel.ResultRequest($"SELECT count(*) FROM `PlanningTasks` pt WHERE pt.`IdPlan` = '{value}'"));
                id = value;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public string Commit
        {
            get;
            set;
        }

        public decimal TargetAmount
        {
            get;
            set;
        }

        public int CountTask { get; set; }
    }
}
