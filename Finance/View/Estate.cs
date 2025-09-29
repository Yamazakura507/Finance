

namespace Finance.View
{
    public class Estate : Abstract.AbstractViewStatus<Estate> 
    {
        private int idTypeEstate;
        private int idStatusEstate;
        private int idOwner;

        public int IdTypeEstate
        {
            get => idTypeEstate;
            set
            {
                if (idTypeEstate != value)
                {
                    EstateType = GetModel<EstateType>(value);
                    idTypeEstate = value;
                }
            }
        }

        public int IdStatusEstate
        {
            get => idStatusEstate;
            set
            {
                if (idStatusEstate != value)
                {
                    EstateStatus = GetModel<EstateStatus>(value);
                    idStatusEstate = value;
                }
            }
        }

        public int IdOwner
        {
            get => idOwner;
            set
            {
                if (idOwner != value)
                {
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
