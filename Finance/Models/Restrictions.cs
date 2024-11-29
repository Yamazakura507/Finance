using Finance.Classes;

namespace Finance.Models
{
    public class Restrictions : DBModel
    {
        private int id;
        private string name;
        private string commit;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Restrictions>("Id", value);
                }
                id = value;
            }
        }

        public string Name
        {
            get => !IsGet ? GetParametrs<string>("Name", this.GetType()) : name;
            set
            {
                if (value.Length > 100)
                {
                    throw new Exception("Наименование не должено превышать 100 символов!");
                }
                else
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new Exception("Наименование не может быть пустым!");
                    }
                    else
                    {
                        if (!IsGet)
                        {
                            SetParametrs<Restrictions>("Name", value);
                        }
                    }
                    name = value;
                }
            }
        }

        public string Commit
        {
            get => !IsGet ? GetParametrs<string>("Commit", this.GetType()) : commit;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Restrictions>("Commit", String.IsNullOrEmpty(value) ? DBNull.Value : value);
                }

                commit = value;
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
                base.DeleteModel<Restrictions>(this.Id);
            }
            else
            {
                base.DeleteModel<Restrictions>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Restrictions>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Restrictions>(parametrs, Id, WhereCollection);
            }
        }
    }
}
