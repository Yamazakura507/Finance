

namespace Finance.Models
{
    public class LibAddress : Abstract.AbstractModel<LibAddress> 
    {
        private string address;

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
    }
}
