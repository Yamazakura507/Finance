
namespace Finance.View
{
    public class Debtor : Abstract.AbstractViewStatus<Debtor>
    {
        private int idStatusDebtor;

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
                DebtorStatus = GetModel<DebtorStatus>(value);
                idStatusDebtor = value;
            }
        }

        public DebtorStatus DebtorStatus { get; private set; }
    }
}
