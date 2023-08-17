using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Areas.Admin.ViewModels
{
    public class OrderDetailViewModel
    {
        public string ProductName;
        public string ColorName;
        public string SizeName;
        public OrderDetail OrderDetail { get; set; }

       
    }
}
