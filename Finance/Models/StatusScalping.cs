using Finance.Classes;

namespace Finance.Models
{
    public class StatusScalping : DBModel
    {
        private int id;
        private string name;
        private string description;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<StatusScalping>("Id", value);
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
                        SetParametrs<StatusScalping>("Name", value);
                    }
                    name = value;
            }
        }

        public string Description
        {
            get => !IsGet ? GetParametrs<string>("Description", this.GetType()) : description;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<StatusScalping>("Description", value);
                }
                description = value;
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
                base.DeleteModel<StatusScalping>(this.Id);
            }
            else
            {
                base.DeleteModel<StatusScalping>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<StatusScalping>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<StatusScalping>(parametrs, Id, WhereCollection);
            }
        }
    }
}
