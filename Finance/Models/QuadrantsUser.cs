using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class QuadrantsUser : DBModel
    {
        private int id;
        private int idUser;
        private int idQuadrants;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<QuadrantsUser>("Id", value);
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
                    SetParametrs<QuadrantsUser>("IdUser", value);
                }

                User = GetModel<Users>(value);
                idUser = value;
            }
        }

        public int IdQuadrants
        {
            get => !IsGet ? GetParametrs<int>("IdQuadrants", this.GetType()) : idQuadrants;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<QuadrantsUser>("IdQuadrants", value);
                }

                Quadrants = GetModel<Quadrants>(value);
                idQuadrants = value;
            }
        }

        [XmlIgnore]
        public Users User { get; private set; }
        [XmlIgnore]
        public Quadrants Quadrants { get; private set; }

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
                base.DeleteModel<QuadrantsUser>(this.Id);
            }
            else
            {
                base.DeleteModel<QuadrantsUser>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<QuadrantsUser>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<QuadrantsUser>(parametrs, Id, WhereCollection);
            }
        }
    }
}
