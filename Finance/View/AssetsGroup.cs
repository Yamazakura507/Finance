
namespace Finance.View
{
    public class AssetsGroup
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Commit
        {
            get;
            set;
        }

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
