using Finance.Classes;

namespace Finance.Models
{
    public class TypeObject : Abstract.AbstractModelStatus<TypeObject>
    {
        private string objectName;

        public string ObjectName
        {
            get => !IsGet ? GetParametrs<string>("ObjectName", this.GetType()) : objectName;
            set
            {
                if (objectName != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<TypeObject>("ObjectName", value);
                    }
                    objectName = value;
                }
            }
        }

        private new string Description { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
