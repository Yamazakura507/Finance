using Finance.Classes;

namespace Finance.View
{
    public class Tasks : DBModel
    {
        private int id;

        public int Id
        {
            get => id;
            set
            {
                CountTask = Convert.ToInt32(DBModel.ResultRequest($"SELECT count(*) FROM `TaskJobs` pt WHERE pt.`IdTask` = '{value}'"));
                IsDone = Convert.ToBoolean(DBModel.ResultRequest($"SELECT count(*) = 0 FROM `TaskJobs` pt INNER JOIN `Jobs` j ON not j.`IsDone` AND j.`Id` = pt.`IdJob` WHERE pt.`IdTask` = '{value}'"));
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

        public decimal DesiredResult
        {
            get;
            set;
        }

        public int CountTask { get; set; }
        public bool IsDone { get; set; }
    }
}
