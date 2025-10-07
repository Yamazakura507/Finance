
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.Models
{
    public class Owners : Abstract.AbstractModel<Owners>, INotifyPropertyChanged
    {
        private string fullName;
        private string seriesPass;
        private string numberPass;
        private string departmentCodePass;
        private string orgIssuedPass;
        private string phone;
        private int? postIndex;
        private string email;
        private int idBirthdayAddress;
        private int idRegAddress;
        private DateTime dateRegPass;
        private DateTime birthday;
        private bool ownerIsUser;


        public string FullName
        {
            get => !IsGet ? GetParametrs<string>("FullName", this.GetType()) : fullName;
            set
            {
                if (fullName != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("FullName", value);
                    }
                    fullName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SeriesPass
        {
            get => !IsGet ? GetParametrs<string>("SeriesPass", this.GetType()) : seriesPass;
            set
            {
                if (seriesPass != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("SeriesPass", value);
                    }
                    seriesPass = value;
                }
            }
        }

        public string NumberPass
        {
            get => !IsGet ? GetParametrs<string>("NumberPass", this.GetType()) : numberPass;
            set
            {
                if (numberPass != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("NumberPass", value);
                    }
                    numberPass = value;
                }
            }
        }

        public DateTime DateRegPass
        {
            get => !IsGet ? GetParametrs<DateTime>("DateRegPass", this.GetType()) : dateRegPass;
            set
            {
                if (dateRegPass != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("DateRegPass", value);
                    }
                    dateRegPass = value;
                }
            }
        }

        public string DepartmentCodePass
        {
            get => !IsGet ? GetParametrs<string>("DepartmentCodePass", this.GetType()) : departmentCodePass;
            set
            {
                if (departmentCodePass != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("DepartmentCodePass", value);
                    }
                    departmentCodePass = value;
                }
            }
        }

        public string OrgIssuedPass
        {
            get => !IsGet ? GetParametrs<string>("OrgIssuedPass", this.GetType()) : orgIssuedPass;
            set
            {
                if (orgIssuedPass != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("OrgIssuedPass", value);
                    }
                    orgIssuedPass = value;
                }
            }
        }

        public DateTime Birthday
        {
            get => !IsGet ? GetParametrs<DateTime>("Birthday", this.GetType()) : birthday;
            set
            {
                if (birthday != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("Birthday", value);
                    }
                    birthday = value;
                }
            }
        }

        public int IdBirthdayAddress
        {
            get => !IsGet ? GetParametrs<int>("IdBirthdayAddress", this.GetType()) : idBirthdayAddress;
            set
            {
                if (idBirthdayAddress != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("IdBirthdayAddress", value);
                    }

                    BirthdayAddress = GetModel<View.LibAddress>(value);
                    idBirthdayAddress = value;
                }
            }
        }

        public int IdRegAddress
        {
            get => !IsGet ? GetParametrs<int>("IdRegAddress", this.GetType()) : idRegAddress;
            set
            {
                if (idRegAddress != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("IdRegAddress", value);
                    }

                    RegAddress = GetModel<View.LibAddress>(value);
                    idRegAddress = value;
                }
            }
        }

        public bool OwnerIsUser
        {
            get => !IsGet ? GetParametrs<bool>("OwnerIsUser", this.GetType()) : ownerIsUser;
            set
            {
                if (ownerIsUser != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("OwnerIsUser", value);
                    }
                    ownerIsUser = value;
                }
            }
        }

        public string Phone
        {
            get => !IsGet ? GetParametrs<string>("Phone", this.GetType()) : phone;
            set
            {
                if (phone != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("Phone", String.IsNullOrEmpty(value) ? DBNull.Value : value);
                    }
                    phone = value;
                }
            }
        }

        public int? PostIndex
        {
            get => !IsGet ? GetParametrs<int?>("PostIndex", this.GetType()) : postIndex;
            set
            {
                if (postIndex != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<Owners>("PostIndex", value is null ? DBNull.Value : value);
                    }
                    postIndex = value;
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
                        SetParametrs<Owners>("Email", String.IsNullOrEmpty(value) ? DBNull.Value : value);
                    }
                    email = value;
                }
            }
        }


        public View.LibAddress BirthdayAddress { get; private set; }
        public View.LibAddress RegAddress { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
