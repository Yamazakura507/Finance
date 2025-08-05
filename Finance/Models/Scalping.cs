using Finance.Classes;

namespace Finance.Models
{
    public class Scalping : DBModel
    {
        private int id;
        private string name;
        private string description;
        private decimal margin;
        private decimal marginBeforeTax;
        private decimal commissing;
        private int idUser;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Scalping>("Id", value);
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
                        SetParametrs<Scalping>("Name", value);
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
                    SetParametrs<Scalping>("Description", value);
                }
                description = value;
            }
        }

        public decimal Margin
        {
            get => !IsGet ? GetParametrs<decimal>("Margin", this.GetType()) : margin;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Scalping>("Margin", value);
                }
                margin = value;
            }
        }

        public decimal MarginBeforeTax
        {
            get => !IsGet ? GetParametrs<decimal>("MarginBeforeTax", this.GetType()) : marginBeforeTax;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Scalping>("MarginBeforeTax", value);
                }
                marginBeforeTax = value;
            }
        }

        public decimal Commissing
        {
            get => !IsGet ? GetParametrs<decimal>("Commissing", this.GetType()) : commissing;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Scalping>("Commissing", value);
                }
                commissing = value;
            }
        }

        public int IdUser
        {
            get => !IsGet ? GetParametrs<int>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Scalping>("IdUser", value);
                }

                Users = GetModel<Users>(value);
                idUser = value;
            }
        }

        public Users Users { get; private set; }

        public override T GetParametrs<T>(string param, Type typeTb, int? Id = null)
        {
            return base.GetParametrs<T>(param, typeTb, id);
        }

        public override void SetParametrs<T>(string param, object value, int? Id = null)
        {
            if (new[] { "Name", "Description" }.Contains(param))
                base.SetParametrs<T>(param, value, id);
            else
                return;
        }

        public override void DeleteModel<T>(int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.DeleteModel<Scalping>(this.Id);
            }
            else
            {
                base.DeleteModel<Scalping>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Scalping>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Scalping>(parametrs, Id, WhereCollection);
            }
        }
    }
}
