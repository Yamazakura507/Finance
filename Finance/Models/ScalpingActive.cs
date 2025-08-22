using Finance.Classes;

namespace Finance.Models
{
    public class ScalpingActive : Abstract.AbstractModelStatus<ScalpingActive>
    {
        private int countInFutures;
        private decimal goShort;
        private decimal goLong;
        private decimal priceStep;
        private int idTypeCommission;

        public int CountInFutures
        {
            get => !IsGet ? GetParametrs<int>("CountInFutures", this.GetType()) : countInFutures;
            set
            {
                if (countInFutures != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingActive>("CountInFutures", value);
                    }
                    countInFutures = value;
                }
            }
        }

        public decimal GOShort
        {
            get => !IsGet ? GetParametrs<decimal>("GOShort", this.GetType()) : goShort;
            set
            {
                if (goShort != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingActive>("GOShort", value);
                    }
                    goShort = value;
                }
            }
        }

        public decimal GOLong
        {
            get => !IsGet ? GetParametrs<decimal>("GOLong", this.GetType()) : goLong;
            set
            {
                if (goLong != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingActive>("GOLong", value);
                    }
                    goLong = value;
                }
            }
        }

        public decimal PriceStep
        {
            get => !IsGet ? GetParametrs<decimal>("PriceStep", this.GetType()) : priceStep;
            set
            {
                if (priceStep != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingActive>("PriceStep", value);
                    }
                    priceStep = value;
                }
            }
        }

        public int IdTypeCommission
        {
            get => !IsGet ? GetParametrs<int>("IdTypeCommission", this.GetType()) : idTypeCommission;
            set
            {
                if (idTypeCommission != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<ScalpingActive>("IdTypeCommission", value);
                    }

                    TypeCommission = GetModel<TypeCommission>(value);
                    idTypeCommission = value;
                }
            }
        }

        public TypeCommission TypeCommission { get; private set;}

        private new string Description { get; set; }
    }
}
