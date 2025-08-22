using Finance.Classes;

namespace Finance.View
{
    public class Tasks : Abstract.AbstractViewStatus<Tasks>
    {
        private int id;

        public new int Id
        {
            get => id;
            set
            {
                CountTask = Convert.ToInt32(ResultRequest($"SELECT count(*) FROM `TaskJobs` pt WHERE pt.`IdTask` = '{value}'"));
                IsDone = Convert.ToBoolean(ResultRequest($"SELECT count(*) = 0 FROM `TaskJobs` pt INNER JOIN `Jobs` j ON not j.`IsDone` AND j.`Id` = pt.`IdJob` WHERE pt.`IdTask` = '{value}'"));
                id = value;
            }
        }

        public decimal DesiredResult
        {
            get;
            set;
        }

        public int CountTask { get; set; }
        public bool IsDone { get; set; }
    }
}
