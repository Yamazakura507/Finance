using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class TaskJobs : DBModel
    {
        private int id;
        private int idTask;
        private int idJob;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TaskJobs>("Id", value);
                }
                id = value;
            }
        }

        public int IdTask
        {
            get => !IsGet ? GetParametrs<int>("IdTask", this.GetType()) : idTask ;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TaskJobs>("IdTask", value);
                }

                Task = GetModel<Tasks>(value);
                idTask  = value;
            }
        }

        public int IdJob
        {
            get => !IsGet ? GetParametrs<int>("IdJob", this.GetType()) : idJob;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TaskJobs>("IdJob", value);
                }

                Job = GetModel<Jobs>(value);
                idJob = value;
            }
        }

        [XmlIgnore]
        public Jobs Job { get; private set; }
        [XmlIgnore]
        public Tasks Task { get; private set; }

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            base.SetParametrs<T>(param, value, id);
        }

        public override void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.DeleteModel<TaskJobs>(this.Id);
            }
            else
            {
                base.DeleteModel<TaskJobs>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<TaskJobs>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<TaskJobs>(parametrs, Id, WhereCollection);
            }
        }
    }
}
