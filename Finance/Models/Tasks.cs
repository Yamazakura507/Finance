using Finance.Classes;

namespace Finance.Models
{
    public class Tasks : DBModel
    {
        private int id;
        private string name;
        private string commit;
        private decimal desiredResult;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Tasks>("Id", value);
                }
                id = value;
            }
        }

        public string Name
        {
            get => !IsGet ? GetParametrs<string>("Name", this.GetType()) : name;
            set
            {       
                    if (!IsGet)
                    {
                        SetParametrs<Tasks>("Name", value);
                    }
                    name = value;
            }
        }

        public string Commit
        {
            get => !IsGet ? GetParametrs<string>("Commit", this.GetType()) : commit;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Tasks>("Commit", value);
                }
                commit = value;
            }
        }

        public decimal DesiredResult
        {
            get => !IsGet ? GetParametrs<decimal>("DesiredResult", this.GetType()) : desiredResult;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Tasks>("DesiredResult", value);
                }
                desiredResult = value;
            }
        }

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
                base.DeleteModel<Tasks>(this.Id);
            }
            else
            {
                base.DeleteModel<Tasks>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Tasks>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Tasks>(parametrs, Id, WhereCollection);
            }
        }
    }
}
