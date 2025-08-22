

namespace Finance.Models
{
    public class MoneyTransfers : Abstract.AbstractModel<MoneyTransfers>
    {
        private int idAssets;
        private int idStatusTransfer;
        private DateTime timeTransfer;
        private decimal sum;

        public int IdAssets
        {
            get => !IsGet ? GetParametrs<int>("IdAssets", this.GetType()) : idAssets;
            set
            {
                if (idAssets != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<MoneyTransfers>("IdAssets", value);
                    }

                    Assets = GetModel<Assets>(value);
                    idAssets = value;
                }
            }
        }

        public int IdStatusTransfer
        {
            get => !IsGet ? GetParametrs<int>("IdStatusTransfer", this.GetType()) : idStatusTransfer ;
            set
            {
                if (idStatusTransfer != idStatusTransfer)
                {
                    if (!IsGet)
                    {
                        SetParametrs<MoneyTransfers>("IdStatusTransfer", value);
                    }

                    TransferStatus = GetModel<TransferStatus>(value);
                    idStatusTransfer = value;
                }
            }
        }

        public DateTime TimeTransfer
        {
            get => !IsGet ? GetParametrs<DateTime>("TimeTransfer", this.GetType()) : timeTransfer;
            set
            {
                if (timeTransfer != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<MoneyTransfers>("TimeTransfer", value);
                    }

                    timeTransfer = value;
                }
            }
        }

        public decimal Sum
        {
            get => !IsGet ? GetParametrs<decimal>("Sum", this.GetType()) : sum;
            set
            {
                if (sum != value)
                {
                    if (!IsGet)
                    {
                        SetParametrs<MoneyTransfers>("Sum", value);
                    }

                    sum = value;
                }
            }
        }


        public Assets Assets { get; private set; }
        public TransferStatus TransferStatus { get; private set; }

        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
