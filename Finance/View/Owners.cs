
namespace Finance.View
{
    public class Owners : Abstract.AbstractViewModel
    {
        private int idBirthdayAddress;

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
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string PostIndex
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }


        public LibAddress BirthdayAddress { get; private set; }
        public LibAddress RegAddress { get; private set; }
    }
}
