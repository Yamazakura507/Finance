
using System.ComponentModel;

namespace Finance.Models
{
    public class TableName : Abstract.AbstractModelStatus<TableName>
    {
        private string objectName;
        private int? idTypeObject;

        public string ObjectName
        {
            get => !IsGet ? GetParametrs<string>("ObjectName", this.GetType()) : objectName;
            set
            {
                if (objectName != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<TableName>("ObjectName", value);
                    }
                    objectName = value;
                }
            }
        }

        public int? IdTypeObject
        {
            get => !IsGet ? GetParametrs<int?>("IdTypeObject", this.GetType()) : idTypeObject;
            set
            {
                if (idTypeObject != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<TableName>("IdTypeObject", value is null ? DBNull.Value : value);
                    }

                    TypeObject = value is null ? null : GetModel<TypeObject>(value);
                    idTypeObject = value;
                }
            }
        }

        public TypeObject TypeObject { get; private set; }

        private new string Description { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
