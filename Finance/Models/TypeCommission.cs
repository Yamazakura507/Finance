using Finance.Classes;

namespace Finance.Models
{
    public class TypeCommission : Abstract.AbstractModelStatus<TypeCommission>
    {
        private decimal openCommissing;
        private decimal closeCommissing;
        private byte[] icon;

        public decimal OpenCommission
        {
            get => !IsGet ? GetParametrs<decimal>("OpenCommission", this.GetType()) : openCommissing;
            set
            {
                if (openCommissing != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<TypeCommission>("OpenCommission", value);
                    }
                    openCommissing = value;
                }
            }
        }

        public decimal CloseCommission
        {
            get => !IsGet ? GetParametrs<decimal>("CloseCommission", this.GetType()) : closeCommissing;
            set
            {
                if (closeCommissing != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<TypeCommission>("CloseCommission", value);
                    }
                    closeCommissing = value;
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

        private new string Description { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}