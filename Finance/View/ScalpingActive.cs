using Finance.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.View
{
    public class ScalpingActive : Abstract.AbstractViewStatus<ScalpingActive>, INotifyPropertyChanged
    {
        private int idTypeCommission;
        private TypeCommission typeCommission;
        private string ticker;
        private string tickerConv;

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

        public string Ticker
        {
            get => ticker;
            set
            {
                if (ticker != value)
                {
                    ticker = value;
                    ConvertTicker();
                }
            }
        }

        public string TickerView
        {
            get => tickerConv;
            private set
            {
                if (tickerConv != value)
                {
                    tickerConv = value;
                    OnPropertyChanged();
                }
            }
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

        async private void ConvertTicker()
        {
            TickerView = (await Classes.Converters.ConvertYahooFinancePrice.ConvertAsync(ticker)).ToString();
        }

        private new string Description { get; set; }
    }
}
