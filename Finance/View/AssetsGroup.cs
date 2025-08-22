
namespace Finance.View
{
    public class AssetsGroup : Abstract.AbstractViewStatus<AssetsGroup>
    {
        public byte[] Icon
        {
            get;
            set;
        }

        public decimal? SumAssets { get; set; }
        public decimal? SumPasive { get; set; }
        public int? CountAssets { get; set; }
        public int? CountPasive { get; set; }
    }
}
