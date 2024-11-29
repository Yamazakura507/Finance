using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class DateUser : DBModel
    {
        private int id;
        private int idUser;
        private int idDate;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<DateUser>("Id", value);
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
                    SetParametrs<DateUser>("IdUser", value);
                }

                User = GetModel<Users>(value);
                idUser = value;
            }
        }

        public int IdDate
        {
            get => !IsGet ? GetParametrs<int>("IdDate", this.GetType()) : idDate ;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<DateUser>("IdDate", value);
                }

                DateJournal = GetModel<DateJournal>(value);
                idDate  = value;
            }
        }

        [XmlIgnore]
        public Users User { get; private set; }
        [XmlIgnore]
        public DateJournal DateJournal { get; private set; }

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
                base.DeleteModel<DateUser>(this.Id);
            }
            else
            {
                base.DeleteModel<DateUser>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<DateUser>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<DateUser>(parametrs, Id, WhereCollection);
            }
        }
    }
}
