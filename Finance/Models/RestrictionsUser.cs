using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class RestrictionsUser : DBModel
    {
        private int id;
        private int idUser;
        private int idRestrictions;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<RestrictionsUser>("Id", value);
                }
                id = value;
            }
        }

        public int IdUser
        {
            get => !IsGet ? GetParametrs<int>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<RestrictionsUser>("IdUser", value);
                }

                User = GetModel<Users>(value);
                idUser = value;
            }
        }

        public int IdRestrictions
        {
            get => !IsGet ? GetParametrs<int>("IdRestrictions", this.GetType()) : idRestrictions;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<RestrictionsUser>("IdRestrictions", value);
                }

                Restriction = GetModel<Restrictions>(value);
                idRestrictions = value;
            }
        }

        [XmlIgnore]
        public Users User { get; private set; }
        [XmlIgnore]
        public Restrictions Restriction { get; private set; }

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
                base.DeleteModel<RestrictionsUser>(this.Id);
            }
            else
            {
                base.DeleteModel<RestrictionsUser>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<RestrictionsUser>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<RestrictionsUser>(parametrs, Id, WhereCollection);
            }
        }
    }
}
