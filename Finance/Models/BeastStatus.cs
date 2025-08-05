using Finance.Classes;

namespace Finance.Models
{
    public class BeastStatus : DBModel
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
                    SetParametrs<BeastStatus>("Id", value);
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
                        SetParametrs<BeastStatus>("Name", value);
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
                    SetParametrs<BeastStatus>("Description", value);
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
                base.DeleteModel<BeastStatus>(this.Id);
            }
            else
            {
                base.DeleteModel<BeastStatus>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<BeastStatus>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<BeastStatus>(parametrs, Id, WhereCollection);
            }
        }
    }
}
