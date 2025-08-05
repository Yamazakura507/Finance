using Finance.Classes;

namespace Finance.Models
{
    public class TypeCommission : DBModel
    {
        private int id;
        private string name;
        private decimal openCommissing;
        private decimal closeCommissing;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TypeCommission>("Id", value);
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
                        SetParametrs<TypeCommission>("Name", value);
                    }
                    name = value;
            }
        }

        public decimal OpenCommissing
        {
            get => !IsGet ? GetParametrs<decimal>("OpenCommissing", this.GetType()) : openCommissing;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TypeCommission>("OpenCommissing", value);
                }
                openCommissing = value;
            }
        }

        public decimal CloseCommissing
        {
            get => !IsGet ? GetParametrs<decimal>("CloseCommissing", this.GetType()) : closeCommissing;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TypeCommission>("CloseCommissing", value);
                }
                closeCommissing = value;
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
                base.DeleteModel<TypeCommission>(this.Id);
            }
            else
            {
                base.DeleteModel<TypeCommission>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<TypeCommission>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<TypeCommission>(parametrs, Id, WhereCollection);
            }
        }
    }
}
