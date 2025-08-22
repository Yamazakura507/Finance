using System.Xml.Serialization;

namespace Finance.Models
{
    public class AssetsGroup : Abstract.AbstractModelStatus<AssetsGroup>
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
                        SetParametrs<AssetsGroup>("Icon", value is null ? DBNull.Value : value);
                    }
                    icon = value;
                }
            }
        }
    }
}
