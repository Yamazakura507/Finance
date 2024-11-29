using Finance.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Finance.Models
{
    public class TableName : DBModel
    {
        private int id;
        private string name;
        private string objectName;
        private int? idTypeObject;

        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get => !IsGet ? GetParametrs<int>("Id", this.GetType()) : id;
            set
            {
                if (!IsGet)
                {
                    SetParametrs< TableName> ("Id", value);
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
                    SetParametrs< TableName> ("Name", value);
                }
                name = value;
            }
        }

        public string ObjectName
        {
            get => !IsGet ? GetParametrs<string>("ObjectName", this.GetType()) : objectName;
            set
            {
                if (!IsGet)
                {
                    SetParametrs< TableName> ("ObjectName", value);
                }
                objectName = value;
            }
        }

        public int? IdTypeObject
        {
            get => !IsGet ? GetParametrs<int?>("IdTypeObject", this.GetType()) : idTypeObject;
            set
            {
                if (!IsGet)
                {
                    SetParametrs<TableName>("IdTypeObject", value is null ? DBNull.Value : value);
                }

                TypeObject = value is null ? null : GetModel<TypeObject>(value);
                idTypeObject = value;
            }
        }

        [XmlIgnore]
        public TypeObject TypeObject { get; private set; }

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
                base.DeleteModel<TableName> (this.Id);
            }
            else
            {
                base.DeleteModel<TableName> (Id, WhereCollection);
            }
        }

        public override void UpdateModel<T>(Dictionary<string, object> parametrs, int? Id = null, Dictionary<string, object>? WhereCollection = null)
        {
            if (Id is null && WhereCollection is null)
            {
                base.UpdateModel<TableName>(parametrs, this.Id);
            }
            else
            {
                base.UpdateModel<TableName>(parametrs, Id, WhereCollection);
            }
        }
    }
}
