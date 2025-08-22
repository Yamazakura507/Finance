

namespace Finance.Models
{
    public class GroupingAssets : Abstract.AbstractModel<GroupingAssets>
    {
        private int idAssets;
        private int idGroupAssets ;
        private int idDate ;


        public int IdAssets
        {
            get => !IsGet ? GetParametrs<int>("IdAssets", this.GetType()) : idAssets;
            set
            {
                if (idAssets != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<GroupingAssets>("IdAssets", value);
                    }

                    Assets = GetModel<Assets>(value);
                    idAssets = value;
                }
            }
        }

        public int IdGroupAssets
        {
            get => !IsGet ? GetParametrs<int>("IdGroupAssets", this.GetType()) : idGroupAssets ;
            set
            {
                if (idGroupAssets != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<GroupingAssets>("IdGroupAssets", value);
                    }

                    AssetsGroup = GetModel<AssetsGroup>(value);
                    idGroupAssets = value;
                }
            }
        }

        public int IdDate
        {
            get => !IsGet ? GetParametrs<int>("IdDate", this.GetType()) : idDate;
            set
            {
                if (idDate != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<GroupingAssets>("IdDate", value);
                    }

                    DateJournal = GetModel<DateJournal>(value);
                    idDate = value;
                }
            }
        }

        public Assets Assets { get; private set; }
        public AssetsGroup AssetsGroup { get; private set; }
        public DateJournal DateJournal { get; private set; }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
