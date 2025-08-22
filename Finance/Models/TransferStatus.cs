
namespace Finance.Models
{
    public class TransferStatus : Abstract.AbstractModelStatus<TransferStatus>
    {
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
