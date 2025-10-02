

namespace Finance.Models
{
    public class LibAddress : Abstract.AbstractModel<LibAddress> 
    {
        private string address;
        private string latitude;
        private string lontitude;
        private string addressType;

        public string Address
        {
            get => !IsGet ? GetParametrs<string>("Address", this.GetType()) : address;
            set
            {
                if (address != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LibAddress>("Address", value);
                    }
                    address = value;
                }
            }
        }

        public string Latitude
        {
            get => !IsGet ? GetParametrs<string>("Latitude", this.GetType()) : latitude;
            set
            {
                if (latitude != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LibAddress>("Latitude", value is null ? DBNull.Value : value);
                    }
                    latitude = value;
                }
            }
        }

        public string Lontitude
        {
            get => !IsGet ? GetParametrs<string>("Lontitude", this.GetType()) : lontitude;
            set
            {
                if (lontitude != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LibAddress>("Lontitude", value is null ? DBNull.Value : value);
                    }
                    lontitude = value;
                }
            }
        }

        public string AddressType
        {
            get => !IsGet ? GetParametrs<string>("AddressType", this.GetType()) : addressType;
            set
            {
                if (addressType != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LibAddress>("AddressType", value is null ? DBNull.Value : value);
                    }
                    addressType = value;
                }
            }
        }
    }
}
