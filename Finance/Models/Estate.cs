

namespace Finance.Models
{
    public class Estate : Abstract.AbstractModelStatus<Estate> 
    {
        private int idTypeEstate;
        private int idStatusEstate;
        private int idOwner;

        public int IdTypeEstate
        {
            get => !IsGet ? GetParametrs<int>("IdTypeEstate", this.GetType()) : idTypeEstate;
            set
            {
                if (idTypeEstate != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Estate>("IdTypeEstate", value);
                    }

                    EstateType = GetModel<EstateType>(value);
                    idTypeEstate = value;
                }
            }
        }

        public int IdStatusEstate
        {
            get => !IsGet ? GetParametrs<int>("IdStatusEstate", this.GetType()) : idStatusEstate;
            set
            {
                if (idStatusEstate != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Estate>("IdStatusEstate", value);
                    }

                    EstateStatus = GetModel<EstateStatus>(value);
                    idStatusEstate = value;
                }
            }
        }

        public int IdOwner
        {
            get => !IsGet ? GetParametrs<int>("IdOwner", this.GetType()) : idOwner;
            set
            {
                if (idOwner != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Estate>("IdOwner", value);
                    }

                    Owners = GetModel<Owners>(value);
                    idOwner = value;
                }
            }
        }

        public EstateType EstateType { get; private set; }
        public EstateStatus EstateStatus { get; private set; }
        public Owners Owners { get; private set; }
    }
}
