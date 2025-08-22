using Finance.Classes;
using Finance.Models;

namespace Finance.View
{
    public class MoneyTransfers : Abstract.AbstractViewModel
    {
        private int idStatusTransfer;

        public int IdStatusTransfer
        {
            get => idStatusTransfer;
            set
            {

                TransferStatus = GetModel<TransferStatus>(value);
                idStatusTransfer  = value;
            }
        }

        public DateTime TimeTransfer
        {
            get;
            set;
        }

        public decimal Sum
        {
            get;
            set;
        }

        public TransferStatus TransferStatus { get; set; }
    }
}
