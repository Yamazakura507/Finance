
namespace Finance.Models
{
    public class BeastStatus : Abstract.AbstractModelStatus<BeastStatus>
    {
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
