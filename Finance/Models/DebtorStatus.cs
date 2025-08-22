

namespace Finance.Models
{
    public class DebtorStatus : Abstract.AbstractModelStatus<DebtorStatus>
    {
        private new string Description { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
