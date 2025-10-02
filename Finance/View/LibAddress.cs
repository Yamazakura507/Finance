
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.View
{
    public class LibAddress : Abstract.AbstractViewModel, INotifyPropertyChanged
    {
        protected string address;
        protected string addressType;

        public string Address
        {
            get => address;
            set
            {
                if (address != value)
                {
                    address = value;

                    OnPropertyChanged();
                }
            }
        }

        public string AddressType
        {
            get => addressType;
            set
            {
                if (addressType != value)
                {
                    addressType = value;

                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
