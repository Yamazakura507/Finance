using Finance.Classes;

namespace Finance.Models.Abstract
{
    public abstract class AbstractModel<G> : DBModel
    {
        protected int id;
        protected int? idUser;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", typeof(G)) : id;
            set
            {
                if (id != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<G>("Id", value);
                    }
                    id = value;
                }
            }
        }

        public int? IdUser
        {
            get => !IsGet ? GetParametrs<int?>("IdUser", this.GetType()) : idUser;
            set
            {
                if (idUser != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<G>("IdUser", value is null ? DBNull.Value : value);
                    }

                    User = value is null ? null : GetModel<Users>(value);
                    idUser = value;
                }
            }
        }

        public Users User { get; protected set; }

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
                base.DeleteModel<G>(this.Id);
            }
            else
            {
                base.DeleteModel<G>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<G>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<G>(parametrs, Id, WhereCollection);
            }
        }
    }
}
