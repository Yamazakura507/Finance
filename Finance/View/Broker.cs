using Finance.Classes;
using System.Drawing;

namespace Finance.View
{
    public class Broker : Abstract.AbstractViewStatus<Broker>
    {
        public decimal Commission
        {
            get;
            set;
        }

        public string PikerName { get { return $"{Name}({Commission.ToString("F2")} руб.)"; } set { } }

        public byte[] Icon
        {
            get;
            set;
        }
    }
}
