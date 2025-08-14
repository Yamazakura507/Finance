using Finance.Classes;

namespace Finance.Models
{
    public class Tax : DBModel
    {
        private int id;
        private string name;
        private decimal taxPercent;
        private DateTime date;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Tax>("Id", value);
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
                        SetParametrs<Tax>("Name", value);
                    }
                    name = value;
            }
        }

        public decimal TaxPercent
        {
            get => !IsGet ? GetParametrs<decimal>("TaxPercent", this.GetType()) : taxPercent;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Tax>("TaxPercent", value);
                }
                taxPercent = value;
            }
        }

        public DateTime Date
        {
            get => !IsGet ? GetParametrs<DateTime>("Date", this.GetType()) : date;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Tax>("Date", value);
                }

                date = value;
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
                base.DeleteModel<Tax>(this.Id);
            }
            else
            {
                base.DeleteModel<Tax>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Tax>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Tax>(parametrs, Id, WhereCollection);
            }
        }
    }
}
