
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.Models
{
    public class Users : Abstract.AbstractModel<Users>, INotifyPropertyChanged
    {
        private string email;
        private string login;
        private string password;
        private bool isEducation;


        public string Login
        {
            get => !IsGet ? GetParametrs<string>("Login", this.GetType()) : login;
            set
            {
                if (login != value)
                {
                    if (login != value)
                    {
                        if (!IsGet)
                        {
                            SetParametrs<Users>("Login", value);
                        }

                        login = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public string Password
        {
            get => !IsGet ? GetParametrs<string>("Password", this.GetType()) : password;
            set
            {
                if (password != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Users>("Password", value);
                    }
                    password = value;
                }
            }
        }

        public string Email
        {
            get => !IsGet ? GetParametrs<string>("Email", this.GetType()) : email;
            set
            {
                if (email != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Users>("Email", String.IsNullOrEmpty(value) ? DBNull.Value : value);
                    }
                    email = value;
                }
            }
        }

        public bool IsEducation
        {
            get => !IsGet ? GetParametrs<bool>("IsEducation", this.GetType()) : isEducation;
            set
            {
                if (isEducation != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Users>("IsEducation", value);
                    }
                    isEducation = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
