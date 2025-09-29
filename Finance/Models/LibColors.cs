

namespace Finance.Models
{
    public class LibColors : Abstract.AbstractModelStatus<LibColors> 
    {
        private string myColor;

        public string MyColor
        {
            get => !IsGet ? GetParametrs<string>("MyColor", this.GetType()) : myColor;
            set
            {
                if (myColor != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LibColors>("MyColor", value);
                    }
                    myColor = value;
                }
            }
        }
    }
}
