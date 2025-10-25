

namespace Finance.Models
{
    public class LibLink : Abstract.AbstractModelStatus<LibLink> 
    {
        private string link;
        private bool isMainMenu;

        public string Link
        {
            get => !IsGet ? GetParametrs<string>("Link", this.GetType()) : link;
            set
            {
                if (link != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LibLink>("Link", value);
                    }
                    link = value;
                }
            }
        }

        public bool IsMainMenu
        {
            get => !IsGet ? GetParametrs<bool>("IsMainMenu", this.GetType()) : isMainMenu;
            set
            {
                if (isMainMenu != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<LibLink>("IsMainMenu", value);
                    }
                    isMainMenu = value;
                }
            }
        }

        private new string Description { get; set; }
    }
}
