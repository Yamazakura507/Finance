

namespace Finance.Models
{
    public class GroupingObject : Abstract.AbstractModel<GroupingObject>
    {
        private int idGroup;
        private int idObject;

        public int IdGroup
        {
            get => !IsGet ? GetParametrs<int>("IdGroup", this.GetType()) : idGroup;
            set
            {
                if (idGroup != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<GroupingObject>("IdGroup", value);
                    }

                    GroupObject = GetModel<GroupObject>(value);
                    idGroup = value;
                }
            }
        }

        public int IdObject
        {
            get => !IsGet ? GetParametrs<int>("IdObject", this.GetType()) : idObject;
            set
            {
                if (idObject != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<GroupingObject>("IdObject", value);
                    }

                    TableName = GetModel<TableName>(value);
                    idObject = value;
                }
            }
        }

        public GroupObject GroupObject { get; private set; }
        public TableName TableName { get; private set; }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
