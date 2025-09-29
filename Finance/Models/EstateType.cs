

namespace Finance.Models
{
    public class EstateType : Abstract.AbstractModelStatus<EstateType> 
    {
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
