using Finance.Classes;

namespace Finance.Models
{
    public class Broker : Abstract.AbstractModelStatus<Broker>
    {
        private decimal commission;
        private byte[] icon;

        public decimal Commission
        {
            get => !IsGet ? GetParametrs<decimal>("Commission", this.GetType()) : commission;
            set
            {
                if (commission != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Broker>("Commission", value);
                    }
                    commission = value;
                }
            }
        }

        public byte[] Icon
        {
            get => !IsGet ? GetParametrs<byte[]>("Icon", this.GetType()) : icon;
            set
            {
                if (icon != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<AssetsGroup>("Icon", value is null ? DBNull.Value : value);
                    }
                    icon = value;
                }
            }
        }
    }
}
