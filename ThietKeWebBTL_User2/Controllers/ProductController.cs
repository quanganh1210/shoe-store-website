using Microsoft.AspNetCore.Mvc;
using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;
using X.PagedList;

namespace ThietKeWebBTL_User2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoeWebsiteLtwContext db;
        public ProductController(ShoeWebsiteLtwContext db)
        {
            this.db = db;
        }

        //public IActionResult GetProductByGenderAndCategory(int? page, long genderID, long categoryID, string? productName)
        //{
        //    int pageSize = 8;
        //    int pageNumber = page == null || page < 0 ? 1 : page.Value;
        //    List<Product> lstProduct = (from p in db.Products
        //                                join gp in db.GenderProducts on p.ProductId equals gp.ProductId
        //                                join pc in db.ProductCategories on p.ProductId equals pc.ProductId
        //                                where gp.GenderId == genderID && pc.CategoryId == categoryID
        //                                select p).ToList();
        //    if (!string.IsNullOrEmpty(productName))
        //        lstProduct = lstProduct.Where(x => x.Name.Contains(productName)).ToList();
        //    List<ProductViewModel> lstProductViewModel = new List<ProductViewModel>();
        //    for (int i = 0; i < lstProduct.Count; ++i)
        //    {
        //        ProductDetail? productDetail = (from p in db.Products
        //                                        join pd in db.ProductDetails on p.ProductId equals pd.ProductId
        //                                        where pd.ProductId == lstProduct[i].ProductId
        //                                        select pd).FirstOrDefault();
        //        ProductViewModel productVM = new ProductViewModel
        //        {
        //            Product = lstProduct[i],
        //            ProductDetail = productDetail
        //        };
        //        lstProductViewModel.Add(productVM);
        //    }
        //    PagedList<ProductViewModel> lst = new PagedList<ProductViewModel>(lstProductViewModel, pageNumber, pageSize);

        //    ViewBag.genderID = genderID;
        //    ViewBag.categoryID = categoryID;
        //    return View(lst);
        //}

        public IActionResult GetProductByGenderAndCategory(int? page, long genderID, long categoryID, string? productName)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            List<Product> lstProduct = (from p in db.Products
                                        join gp in db.GenderProducts on p.ProductId equals gp.ProductId
                                        join pc in db.ProductCategories on p.ProductId equals pc.ProductId
                                        where gp.GenderId == genderID && pc.CategoryId == categoryID && p.Status == 1
                                        select p).ToList();
            if (!string.IsNullOrEmpty(productName))
                lstProduct = lstProduct.Where(x => x.Name.Contains(productName)).ToList();
            List<ProductViewModel> lstProductViewModel = new List<ProductViewModel>();
            for (int i = 0; i < lstProduct.Count; ++i)
            {
                var lstProductDetail = (from p in db.Products
                                        join pd in db.ProductDetails on p.ProductId equals pd.ProductId
                                        where pd.ProductId == lstProduct[i].ProductId && pd.Status == 1
                                        select pd).GroupBy(x => new { x.ProductId, x.ColorId }).Select(x => x.First()).ToList();
                ProductViewModel productVM = new ProductViewModel
                {
                    Product = lstProduct[i],
                    ListProductDetail = lstProductDetail
                };
                lstProductViewModel.Add(productVM);
            }
            PagedList<ProductViewModel> lst = new PagedList<ProductViewModel>(lstProductViewModel, pageNumber, pageSize);

            ViewBag.genderID = genderID;
            ViewBag.categoryID = categoryID;
            return View(lst);
        }

        public IActionResult ProductDetail(long productID)
        {
            Product product = db.Products.Find(productID);
            List<Color> lstColor = (from p in db.Products
                                    join pd in db.ProductDetails on p.ProductId equals pd.ProductId
                                    join c in db.Colors on pd.ColorId equals c.ColorId
                                    where p.ProductId == productID && pd.Status == 1
                                    select c).Distinct().ToList();
            ViewBag.color = lstColor;
            List<Size> lstSize = (from p in db.Products
                                  join pd in db.ProductDetails on p.ProductId equals pd.ProductId
                                  join s in db.Sizes on pd.SizeId equals s.SizeId
                                  where p.ProductId == productID && pd.ColorId == lstColor[0].ColorId
                                  select s).Distinct().ToList();

            ViewBag.size = lstSize;
            ProductDetail first = db.ProductDetails.Where(x => x.ProductId == productID && x.ColorId == lstColor[0].ColorId).FirstOrDefault();
            ViewBag.image = (from imgpd in db.ImageProductDetails
                             join image in db.Images on imgpd.ImageId equals image.ImageId
                             where imgpd.ProductDetailId == first.ProductDetailId
                             select image).ToList();
            Decimal? currentPrice = 0;
            if (first.Discount != 0)
            {
                currentPrice = first.UnitSellingPrice * (1 - (Decimal)first.Discount / 100);
                ViewBag.priceBeforeDiscount = "$" + String.Format("{0:0.##}", first.UnitSellingPrice);
                ViewBag.discount = first.Discount.ToString() + "% off";
            }
            else
                currentPrice = first.UnitSellingPrice;
            ViewBag.price = "$" + String.Format("{0:0.##}", currentPrice);
            return View(product);
        }
    }
}
