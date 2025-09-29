

namespace Finance.Models
{
    public class EstateStatus : Abstract.AbstractModelStatus<EstateStatus> 
    {
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
