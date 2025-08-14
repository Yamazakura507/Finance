using Finance.Classes;
using System.Drawing;

namespace Finance.Models
{
    public class TypeCommission : DBModel
    {
        private int id;
        private string name;
        private decimal openCommissing;
        private decimal closeCommissing;
        private byte[] icon;

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

        public decimal OpenCommission
        {
            get => !IsGet ? GetParametrs<decimal>("OpenCommission", this.GetType()) : openCommissing;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TypeCommission>("OpenCommission", value);
                }
                openCommissing = value;
            }
        }

        public decimal CloseCommission
        {
            get => !IsGet ? GetParametrs<decimal>("CloseCommission", this.GetType()) : closeCommissing;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TypeCommission>("CloseCommission", value);
                }
                closeCommissing = value;
            }
        }

        public byte[] Icon
        {
            get => !IsGet ? GetParametrs<byte[]>("Icon", this.GetType()) : icon;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<AssetsGroup>("Icon", value is null ? DBNull.Value : value);
                }
                icon = value;
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