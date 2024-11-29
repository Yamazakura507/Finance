using Finance.Classes;
using Finance.Models;

namespace Finance.View
{
    public class Debtor
    {
        private int idStatusDebtor;

        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public decimal Sum
        {
            get;
            set;
        }
        public int IdStatusDebtor
        {
            get => idStatusDebtor;
            set
            {
                DebtorStatus = DBModel.GetModel<DebtorStatus>(value);
                idStatusDebtor = value;
            }
        }
        public string Commit
        {
            get;
            set;
        }

        public DebtorStatus DebtorStatus { get; private set; }
    }
}
