
namespace Finance.View
{
    public class LibColors : Abstract.AbstractViewStatus<LibColors>
    {
        private string myColor;
        private Color color;

        public string MyColor
        {
            get => myColor;
            set
            {
                if (myColor != value)
                {
                    myColor = value;
                    Colors = Color.FromArgb(value);
                    OnPropertyChanged();
                }
            }
        }

        public Color Colors
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        private new string Description { get; set; }
    }
}
