using Finance.Classes;

namespace Finance.Models
{
    public class ScalpingActive : DBModel
    {
        private int id;
        private int countInFutures;
        private string name;
        private decimal goShort;
        private decimal goLong;
        private decimal priceStep;
        private int idUser;
        private int idTypeCommission;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<ScalpingActive>("Id", value);
                }
                id = value;
            }
        }

        public int CountInFutures
        {
            get => !IsGet ? GetParametrs<int>("CountInFutures", this.GetType()) : countInFutures;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<ScalpingActive>("CountInFutures", value);
                }
                countInFutures = value;
            }
        }

        public string Name
        {
            get => !IsGet ? GetParametrs<string>("Name", this.GetType()) : name;
            set
            {       
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingActive>("Name", value);
                    }
                    name = value;
            }
        }

        public decimal GOShort
        {
            get => !IsGet ? GetParametrs<decimal>("GOShort", this.GetType()) : goShort;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<ScalpingActive>("GOShort", value);
                }
                goShort = value;
            }
        }

        public decimal GOLong
        {
            get => !IsGet ? GetParametrs<decimal>("GOLong", this.GetType()) : goLong;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<ScalpingActive>("GOLong", value);
                }
                goLong = value;
            }
        }

        public decimal PriceStep
        {
            get => !IsGet ? GetParametrs<decimal>("PriceStep", this.GetType()) : priceStep;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<ScalpingActive>("PriceStep", value);
                }
                priceStep = value;
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

        public int IdTypeCommission
        {
            get => !IsGet ? GetParametrs<int>("IdTypeCommission", this.GetType()) : idTypeCommission;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<ScalpingActive>("IdTypeCommission", value);
                }

                TypeCommission = GetModel<TypeCommission>(value);
                idTypeCommission = value;
            }
        }

        public Users Users { get; private set; }

        public TypeCommission TypeCommission { get; private set;}

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
                base.DeleteModel<ScalpingActive>(this.Id);
            }
            else
            {
                base.DeleteModel<ScalpingActive>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<ScalpingActive>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<ScalpingActive>(parametrs, Id, WhereCollection);
            }
        }
    }
}
