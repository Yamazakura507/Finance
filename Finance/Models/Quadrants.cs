using Finance.Classes;

namespace Finance.Models
{
    public class Quadrants : Abstract.AbstractModelStatus<Quadrants>
    {
        private string initial;
        private string downLimit;
        private byte[] icon;

        public string Initial
        {
            get => !IsGet ? GetParametrs<string>("Initial", this.GetType()) : initial;
            set
            {
                if (initial != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Quadrants>("Initial", value);
                    }
                    initial = value;
                }
            }
        }
        public string DownLimit
        {
            get => !IsGet ? GetParametrs<string>("DownLimit", this.GetType()) : downLimit;
            set
            {
                if (downLimit != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Quadrants>("DownLimit", value);
                    }
                    downLimit = value;
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
                        SetParametrs<Quadrants>("Icon", value);
                    }
                    icon = value;
                }
            }
        }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
