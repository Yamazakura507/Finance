using Finance.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.View
{
    public class ScalpingActive : Abstract.AbstractViewStatus<ScalpingActive>, INotifyPropertyChanged
    {
        private int idTypeCommission;
        private TypeCommission typeCommission;

        public int CountInFutures
        {
            get;
            set;
        }

        public decimal GOShort
        {
            get;
            set;
        }

        public decimal GOLong
        {
            get;
            set;
        }

        public decimal PriceStep
        {
            get;
            set;
        }

        public int IdTypeCommission
        {
            get => idTypeCommission;
            set
            {
                TypeCommission = GetModel<TypeCommission>(value);
                idTypeCommission = value;
            }
        }

        public TypeCommission TypeCommission 
        { 
            get => typeCommission; 
            private set 
            {
                if (idTypeCommission != value.Id)
                {
                    typeCommission = value;
                    OnPropertyChanged();
                }
            } 
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private new string Description { get; set; }
    }
}
