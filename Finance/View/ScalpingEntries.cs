using Finance.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance.View
{
    public class ScalpingEntries : DBModel, INotifyPropertyChanged
    {
        private int idStatusScalping;
        private int idBroker;
        private int? idTypeCommission;
        private int idTax;
        private int? idScalpingActive;
        private decimal margin;
        private decimal marginBeforeTax;
        private decimal commission;
        private Models.StatusScalping statusScalping;
        private DateTime? dateExit;
        private Color color;


        public delegate void MessageEventHandler(string message);
        public static event MessageEventHandler ErrorEvent;

        public int Id
        {
            get;
            set;
        }

        public int CountLot
        {
            get;
            set;
        }

        public int? CountInFutures
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public decimal PriceEntry
        {
            get;
            set;
        }

        public decimal PriceExit
        {
            get;
            set;
        }

        public decimal PointZero
        {
            get;
            set;
        }

        public DateTime? DateExit
        {
            get => dateExit;
            private set
            {
                if (dateExit != value)
                {
                    dateExit = value;
                    OnPropertyChanged();
                }
            }
        }

        public int IdTax
        {
            get => idTax;
            set
            {
                Tax = GetModel<Tax>(value);
                idTax = value;
            }
        }

        public int IdStatusScalping
        {
            get => idStatusScalping;
            set
            {
                StatusScalping = GetModel<Models.StatusScalping>(value);
                idStatusScalping = value;
            }
        }

        public int? IdTypeCommission
        {
            get => idTypeCommission;
            set
            {
                TypeCommission = GetModel<TypeCommission>(value);
                idTypeCommission = value;
            }
        }

        public int IdBeastStatus
        {
            get;
            set;
        }

        public int IdBroker
        {
            get => idBroker;
            set
            {
                Broker = GetModel<View.Broker>(value);
                idBroker = value;
            }
        }

        public int? IdScalpingActive
        {
            get => idScalpingActive;
            set
            {
                ScalpingActive = value is null ? null : GetModel<ScalpingActive>(value);
                idScalpingActive = value;
            }
        }

        public decimal Margin
        {
            get => margin;
            set
            {
                IsProfit = value >= 0;
                margin = value;
            }
        }

        public decimal MarginBeforeTax
        {
            get => marginBeforeTax;
            set
            {
                IsProfitBeforeTax = value >= 0;
                marginBeforeTax = value;
            }
        }

        public decimal Commissing
        {
            get => commission;
            set
            {
                IsCommission = value > 0;
                commission = value;
            }
        }

        public TypeCommission TypeCommission { get; private set;}

        public Tax Tax { get; private set;}

        public Scalping Scalping { get; private set;}

        public ScalpingActive ScalpingActive { get; private set;}

        public Models.StatusScalping StatusScalping
        {
            get => statusScalping;
            private set
            {
                if (idStatusScalping != value.Id)
                {
                    statusScalping = value;
                    DateExit = !IsGet ? GetParametrs<DateTime?>("DateExit", this.GetType(), Id) : dateExit;

                    switch (value.Id)
                    {
                        case 1: ShadowColorBrush = IsProfit ? Colors.LawnGreen : Colors.OrangeRed; break;
                        case 2: ShadowColorBrush = Colors.Yellow; break;
                        case 3: ShadowColorBrush = Colors.Cyan; break;
                    }

                    OnPropertyChanged();
                }
            }
        }

        public Broker Broker { get; private set;}


        public bool IsProfit { get; set; }
        public bool IsProfitBeforeTax { get; set; }
        public bool IsCommission { get; set; }


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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
