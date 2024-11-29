using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class TimeCoast : DBModel
    {
        private int id;
        private int idUser;
        private string name;
        private string commit;
        private int minute;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TimeCoast>("Id", value);
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
                    SetParametrs<TimeCoast>("Name", value);
                }
                name = value;
            }
        }
        public string Commit
        {
            get => !IsGet ? GetParametrs<string>("Commit", this.GetType()) : commit;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TimeCoast>("Commit", value);
                }
                commit = value;
            }
        }
        public int IdUser
        {
            get => !IsGet ? GetParametrs<int>("IdUser", this.GetType()) : idUser;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TimeCoast>("IdUser", value);
                }

                User = GetModel<Users>(value);
                idUser = value;
            }
        }
        public int Minute
        {
            get => !IsGet ? GetParametrs<int>("Minute", this.GetType()) : minute;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TimeCoast>("Minute", value);
                }
                minute = value;
            }
        }

        [XmlIgnore]
        public Users User { get; private set; }
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
                base.DeleteModel<TimeCoast>(this.Id);
            }
            else
            {
                base.DeleteModel<TimeCoast>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<TimeCoast>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<TimeCoast>(parametrs, Id, WhereCollection);
            }
        }
    }
}
