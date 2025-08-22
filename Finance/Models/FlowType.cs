
namespace Finance.Models
{
    public class FlowType : Abstract.AbstractModelStatus<FlowType>
    {
        private new string Description { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
