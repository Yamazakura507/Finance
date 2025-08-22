
namespace Finance.Models
{
    public class ObjectRestrict : Abstract.AbstractModelStatus<ObjectRestrict>
    {
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
