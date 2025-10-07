
namespace Finance.View
{
    public class Owners : Abstract.AbstractViewModel
    {
        private int idBirthdayAddress;
        private bool ownerIsUser;

        public string FullName
        {
            get;
            set;
        }

        public DateTime Birthday
        {
            get;
            set;
        }

        public int IdBirthdayAddress
        {
            get => idBirthdayAddress;
            set
            {
                if (idBirthdayAddress != value)
                {
                    BirthdayAddress = GetModel<LibAddress>(value);
                    idBirthdayAddress = value;
                }
            }
        }

        public bool OwnerIsUser
        {
            get => ownerIsUser;
            set
            {
                if (ownerIsUser != value)
                {
                    ownerIsUser = value;
                    ShadowColorBrush = value ? Colors.DarkOliveGreen : Colors.Transparent;
                }
            }
        }

        public string Phone
        {
            get;
            set;
        }

        public int? PostIndex
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public Color ShadowColorBrush
        {
            get;
            private set;
        }


        public LibAddress BirthdayAddress { get; private set; }
    }
}
