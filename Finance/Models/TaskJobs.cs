using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class TaskJobs : Abstract.AbstractModel<TaskJobs>
    {
        private int idTask;
        private int idJob;

        public int IdTask
        {
            get => !IsGet ? GetParametrs<int>("IdTask", this.GetType()) : idTask ;
            set
            {
                if (idTask != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<TaskJobs>("IdTask", value);
                    }

                    Task = GetModel<Tasks>(value);
                    idTask = value;
                }
            }
        }

        public int IdJob
        {
            get => !IsGet ? GetParametrs<int>("IdJob", this.GetType()) : idJob;
            set
            {
                if (idJob != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<TaskJobs>("IdJob", value);
                    }

                    Job = GetModel<Jobs>(value);
                    idJob = value;
                }
            }
        }

        public Jobs Job { get; private set; }
        public Tasks Task { get; private set; }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
