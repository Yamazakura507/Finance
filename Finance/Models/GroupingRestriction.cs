using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class GroupingRestriction : DBModel
    {
        private int id;
        private int idGroup;
        private int idRestriction;
        private int idObjectRestriction;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingRestriction>("Id", value);
                }
                id = value;
            }
        }

        public int IdRestiction
        {
            get => !IsGet ? GetParametrs<int>("IdObject", this.GetType()) : idRestriction;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingRestriction>("IdObject", value);
                }

                Restriction = GetModel<Restrictions>(value);
                idRestriction = value;
            }
        }

        public int IdGroup
        {
            get => !IsGet ? GetParametrs<int>("IdGroup", this.GetType()) : idGroup;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingRestriction>("IdGroup", value);
                }

                GroupObject = GetModel<GroupObject>(value);
                idGroup = value;
            }
        }

        public int IdObjectRestiction
        {
            get => !IsGet ? GetParametrs<int>("IdObjectRestiction", this.GetType()) : idObjectRestriction;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingRestriction>("IdObjectRestiction", value);
                }

                ObjectRestrict = GetModel<ObjectRestrict>(value);
                idObjectRestriction = value;
            }
        }

        [XmlIgnore]
        public GroupObject GroupObject { get; private set; }
        [XmlIgnore]
        public Restrictions Restriction { get; private set; }
        [XmlIgnore]
        public ObjectRestrict ObjectRestrict { get; private set; }

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
                base.DeleteModel<GroupingRestriction>(this.Id);
            }
            else
            {
                base.DeleteModel<GroupingRestriction>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<GroupingRestriction>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<GroupingRestriction>(parametrs, Id, WhereCollection);
            }
        }
    }
}
