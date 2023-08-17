using ThietKeWebBTL_User2.Models;

namespace ThietKeWebBTL_User2.ViewModels
{
    [Serializable]
    public class CartItem
    {
        public long ProductID { get; set; }
        public long ProductDetailID { get; set; }
        public Product Product { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public int Quantity { get; set; }
        public string SizeName { get; set; }
    }
}
