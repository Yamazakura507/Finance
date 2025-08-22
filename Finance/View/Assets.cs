
namespace Finance.View
{
    public class Assets : Abstract.AbstractViewStatus<Assets>
    {
        private int idFlowType;

        public bool IsStability
        {
            get;
            set;
        }
        public int IdFlowType
        {
            get => idFlowType;
            set
            {
                FlowType = GetModel<FlowType>(value);
                idFlowType = value;
            }
        }
        public decimal Sum
        {
            get;
            set;
        }
        public bool IsAsset
        {
            get;
            set;
        }

        public FlowType FlowType { get; set; }
    }
}
