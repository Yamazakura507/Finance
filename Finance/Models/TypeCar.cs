

namespace Finance.Models
{
    public class TypeCar : Abstract.AbstractModelStatus<TypeCar> 
    {
        private new int? IdUser { get; set; }
        private new Users User { get; set; }
    }
}
