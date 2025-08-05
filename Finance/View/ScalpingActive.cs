using Finance.Classes;

namespace Finance.View
{
    public class ScalpingActive : DBModel
    {
        private int idTypeCommission;

        public int Id
        {
            get;
            set;
        }

        public int CountInFutures
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public decimal GoShort
        {
            get;
            set;
        }

        public decimal GoLong
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
                TypeCommission = GetModel<Models.TypeCommission>(value);
                idTypeCommission = value;
            }
        }

        public Models.TypeCommission TypeCommission { get; private set;}
    }
}
