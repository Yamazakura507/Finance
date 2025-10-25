
using Finance.Classes;

namespace Finance.View
{
    public class LibLink : Abstract.AbstractViewStatus<LibLink>
    {
        private string link;
        private ImageSource icon;
        private bool isMainMenu;
        private Color color;

        public string Link
        {
            get => link;
            set
            {
                if (link != value)
                {
                    link = value;
                    LoadImage().Wait();
                    OnPropertyChanged();
                }
            }
        }

        public bool IsMainMenu
        {
            get => isMainMenu;
            set
            {
                if (isMainMenu != value)
                {
                    isMainMenu = value;
                    ShadowColorBrush = value ? Colors.DarkOliveGreen : Colors.Transparent;
                    OnPropertyChanged();
                }
            }
        }

        public ImageSource Icon 
        {
            get => icon;
            set
            {
                if (icon != value)
                {
                    icon = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color ShadowColorBrush
        {
            get => color;
            private set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        private new string Description { get; set; }

        async private Task LoadImage()
        {
            Icon = await new Uri(link).ImageLinked(true);
        }
    }
}
