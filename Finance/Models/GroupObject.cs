using Finance.Classes;

namespace Finance.Models
{
    public class GroupObject : Abstract.AbstractModelStatus<GroupObject>
    {
        private new string Description { get; set; }
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
