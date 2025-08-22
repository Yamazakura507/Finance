

namespace Finance.Models
{
    public class CreditStatus : Abstract.AbstractModelStatus<CreditStatus> 
    {
        private new string Description { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
