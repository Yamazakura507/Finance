using Finance.Models;
using DocxDocWorkingLib.Interface;

namespace Finance.Pages.WorkPage.Shablons.Models
{
    public class DocDkpInfo : IDoc
    {
        public View.LibAddress DocAddress { get; set; }

        public DateTime DocDate { get; set; } = DateTime.Now;

        public Owners ByerInfo { get; set; }

        public Owners SellerInfo { get; set; }

        public Car CarInfo { get; set; }
    }
}
