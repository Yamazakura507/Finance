using Finance.Classes;

namespace Finance.Models
{
    public class Broker : DBModel
    {
        private int id;
        private int idUser;
        private string name;
        private decimal commission;
        private string description;
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
                    SetParametrs<Broker>("Id", value);
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
                        SetParametrs<Broker>("Name", value);
                    }
                    name = value;
            }
        }

        public decimal Commission
        {
            get => !IsGet ? GetParametrs<decimal>("Commission", this.GetType()) : commission;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Broker>("Commission", value);
                }
                commission = value;
            }
        }

        public string Description
        {
            get => !IsGet ? GetParametrs<string>("Description", this.GetType()) : description;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Broker>("Description", value);
                }
                description = value;
            }
        }

        public int IdUser
        {
            get => !IsGet ? GetParametrs<int>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<Broker>("IdUser", value);
                }

                Users = GetModel<Users>(value);
                idUser = value;
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

        public Users Users { get; private set; }

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
                base.DeleteModel<Broker>(this.Id);
            }
            else
            {
                base.DeleteModel<Broker>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<Broker>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<Broker>(parametrs, Id, WhereCollection);
            }
        }
    }
}
