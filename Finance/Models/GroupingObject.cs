using Finance.Classes;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class GroupingObject : DBModel
    {
        private int id;
        private int idGroup;
        private int idObject;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingObject>("Id", value);
                }
                id = value;
            }
        }

        public int IdGroup
        {
            get => !IsGet ? GetParametrs<int>("IdGroup", this.GetType()) : idGroup;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingObject>("IdGroup", value);
                }

                GroupObject = GetModel<GroupObject>(value);
                idGroup = value;
            }
        }

        public int IdObject
        {
            get => !IsGet ? GetParametrs<int>("IdObject", this.GetType()) : idObject;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<GroupingObject>("IdObject", value);
                }

                TableName = GetModel<TableName>(value);
                idObject = value;
            }
        }

        [XmlIgnore]
        public GroupObject GroupObject { get; private set; }
        [XmlIgnore]
        public TableName TableName { get; private set; }

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
                base.DeleteModel<GroupingObject>(this.Id);
            }
            else
            {
                base.DeleteModel<GroupingObject>(Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<GroupingObject>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<GroupingObject>(parametrs, Id, WhereCollection);
            }
        }
    }
}
