using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class PlanningTasks : DBModel
    {
        private int id;
        private int idTask;
        private int idPlan;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<PlanningTasks>("Id", value);
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
                    SetParametrs<PlanningTasks>("IdTask", value);
                }

                Task = GetModel<Tasks>(value);
                idTask  = value;
            }
        }

        public int IdPlan
        {
            get => !IsGet ? GetParametrs<int>("IdPlan", this.GetType()) : idPlan;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<PlanningTasks>("IdPlan", value);
                }

                Plan = GetModel<PlanningJournal>(value);
                idPlan = value;
            }
        }

        [XmlIgnore]
        public PlanningJournal Plan { get; private set; }
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
                base.DeleteModel<PlanningTasks>(this.Id);
            }
            else
            {
                base.DeleteModel<PlanningTasks>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<PlanningTasks>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<PlanningTasks>(parametrs, Id, WhereCollection);
            }
        }
    }
}
