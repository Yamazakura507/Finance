
namespace Finance.View
{
    public class TypeCommission : Abstract.AbstractViewStatus<TypeCommission>
    {
        public decimal OpenCommission
        {
            get;
            set;
        }

        public decimal CloseCommission
        {
            get;
            set;
        }

        public byte[] Icon
        {
            get;
            set;
        }

        private new string Description { get; set; }
    }
}
