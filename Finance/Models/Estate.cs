

namespace Finance.Models
{
    public class Estate : Abstract.AbstractModelStatus<Estate> 
    {
        private int idTypeEstate;
        private int idStatusEstate;
        private int idOwner;
        private int? idCar;
        private View.Car car;
        private View.Owners owner;

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
                        IdCar = null;
                    }

                    EstateType = GetModel<View.EstateType>(value);
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

                    EstateStatus = GetModel<View.EstateStatus>(value);
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

                    Owners = GetModel<View.Owners>(value);
                    idOwner = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdCar
        {
            get => !IsGet ? GetParametrs<int?>("IdCar", this.GetType()) : idOwner;
            set
            {
                if (idCar != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Estate>("IdCar", value is null ? DBNull.Value : value);
                    }

                    Car = GetModel<View.Car>(value);
                    idCar = value;
                }
            }
        }

        public View.EstateType EstateType { get; private set; }
        public View.EstateStatus EstateStatus { get; private set; }
        public View.Car Car
        {
            get => car;
            private set
            {
                if (idCar != value.Id)
                {
                    car = value;
                    OnPropertyChanged();
                }
            }
        }
        public View.Owners Owners 
        { 
            get => owner;
            private set
            {
                if (idOwner != value.Id)
                {
                    owner = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
