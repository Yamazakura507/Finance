

using System.Drawing;

namespace Finance.Models
{
    public class EstateType : Abstract.AbstractModelStatus<EstateType> 
    {
        private byte[] icon;

        public byte[] Icon
        {
            get => !IsGet ? GetParametrs<byte[]>("Icon", this.GetType()) : icon;
            set
            {
                if (icon != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<EstateType>("Icon", value is null ? DBNull.Value : value);
                    }
                    icon = value;
                }
            }
        }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
