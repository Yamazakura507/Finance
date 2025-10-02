

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.View
{
    public class Estate : Abstract.AbstractViewStatus<Estate>, INotifyPropertyChanged
    {
        private int idTypeEstate;
        private int idStatusEstate;
        private int idOwner;
        private EstateStatus estateStatus;

        public int IdTypeEstate
        {
            get => idTypeEstate;
            set
            {
                if (idTypeEstate != value)
                {
                    EstateType = GetModel<EstateType>(value);
                    idTypeEstate = value;
                }
            }
        }

        public int IdStatusEstate
        {
            get => idStatusEstate;
            set
            {
                if (idStatusEstate != value)
                {
                    EstateStatus = GetModel<EstateStatus>(value);
                    idStatusEstate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int IdOwner
        {
            get => idOwner;
            set
            {
                if (idOwner != value)
                {
                    Owners = GetModel<Owners>(value);
                    idOwner = value;
                }
            }
        }

        public EstateType EstateType { get; private set; }
        public EstateStatus EstateStatus
        {
            get => estateStatus;
            private set
            {
                if (idStatusEstate != value.Id)
                {
                    estateStatus = value;
                    OnPropertyChanged();
                }
            }
        }
        public Owners Owners { get; private set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
