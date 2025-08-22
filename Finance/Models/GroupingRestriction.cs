using Finance.Classes;

namespace Finance.Models
{
    public class GroupingRestriction : Abstract.AbstractModel<GroupingRestriction>
    {
        private int idGroup;
        private int idRestriction;
        private int idObjectRestriction;

        public int IdRestiction
        {
            get => !IsGet ? GetParametrs<int>("IdObject", this.GetType()) : idRestriction;
            set
            {
                if (id != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<GroupingRestriction>("IdObject", value);
                    }

                    Restriction = GetModel<Restrictions>(value);
                    idRestriction = value;
                }
            }
        }

        public int IdGroup
        {
            get => !IsGet ? GetParametrs<int>("IdGroup", this.GetType()) : idGroup;
            set
            {
                if (id != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<GroupingRestriction>("IdGroup", value);
                    }

                    GroupObject = GetModel<GroupObject>(value);
                    idGroup = value;
                }
            }
        }

        public int IdObjectRestiction
        {
            get => !IsGet ? GetParametrs<int>("IdObjectRestiction", this.GetType()) : idObjectRestriction;
            set
            {
                if (id != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<GroupingRestriction>("IdObjectRestiction", value);
                    }

                    ObjectRestrict = GetModel<ObjectRestrict>(value);
                    idObjectRestriction = value;
                }
            }
        }

        public GroupObject GroupObject { get; private set; }
        public Restrictions Restriction { get; private set; }
        public ObjectRestrict ObjectRestrict { get; private set; }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
