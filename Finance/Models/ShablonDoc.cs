using Finance.Classes;

namespace Finance.Models
{
    public class ShablonDoc : Abstract.AbstractModelStatus<ShablonDoc>
    {
        private byte[] doc;

        public byte[] Doc
        {
            get => !IsGet ? GetParametrs<byte[]>("Doc", this.GetType()) : doc;
            set
            {
                if (doc != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<ShablonDoc>("Doc", value);
                    }
                    doc = value;
                }
            }
        }
    }
}
