using Finance.Classes;

namespace Finance.Models
{
    public class StatusScalping : Abstract.AbstractModelStatus<StatusScalping>
    {
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
