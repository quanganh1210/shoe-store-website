using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductAPIController : ControllerBase
	{
		private readonly ShoeWebsiteLtwContext db;
		public ProductAPIController(ShoeWebsiteLtwContext db)
		{
			this.db = db;
		}
		
		[HttpGet]
		[Route("GetProductDetailID")]
        public long GetProductDetailID(long productID, long colorID, long sizeID)
		{
			ProductDetail productDetail = db.ProductDetails.Where(x => x.ColorId == colorID && x.SizeId == sizeID && x.ProductId == productID).FirstOrDefault();
			return productDetail.ProductDetailId;
			
		}
        [HttpGet]
        [Route("GetProductDetailImage")]
        public List<Image> GetProductDetailImage(long productID, long colorID)
		{
			ProductDetail firstDetail = db.ProductDetails.Where(x => x.ProductId == productID && x.ColorId == colorID).First();
			List<Image> lstImage = (from pd in db.ProductDetails
									join imp in db.ImageProductDetails on pd.ProductDetailId equals imp.ProductDetailId
									join img in db.Images on imp.ImageId equals img.ImageId
									where pd.ProductDetailId == firstDetail.ProductDetailId
									select img).ToList();
			return lstImage;
		}

        [HttpGet]
        [Route("GetProductDetailPrice")]
        public ProductDetailPrice GetProductDetailPrice(long productID, long colorID)
        {
            ProductDetail first = db.ProductDetails.Where(x => x.ProductId == productID && x.ColorId == colorID).FirstOrDefault();
            Decimal? currentPrice = 0;
            string priceBeforeDiscount = "";
            string discount = "";
            if (first.Discount != 0)
            {
                currentPrice = first.UnitSellingPrice * (1 - (Decimal)first.Discount / 100);
                
                priceBeforeDiscount = "$" + String.Format("{0:0.##}", first.UnitSellingPrice);
                discount = first.Discount.ToString() + "% off";
            }
            else
                currentPrice = first.UnitSellingPrice;
            //Decimal? unitSellingPrice = (from pd in db.ProductDetails
            //                        where pd.ProductId == productID && pd.ColorId == colorID
            //                        select pd.UnitSellingPrice).FirstOrDefault();
            //return unitSellingPrice;
            ProductDetailPrice productDetailPrice = new ProductDetailPrice
            {
                CurrentPrice = String.Format("{0:0.##}", currentPrice),
                PriceBeforeDiscount = priceBeforeDiscount,
                Discount = discount
            };
            return productDetailPrice;
            
            
        }

        [HttpGet]
        [Route("GetProductDetailSize")]
        public List<Size> GetProductDetailSize(long productID, long colorID)
        {

			List<Size> lstSize = (from pd in db.ProductDetails
						   join s in db.Sizes on pd.SizeId equals s.SizeId
						   where pd.ProductId == productID && pd.ColorId == colorID
						   select s).ToList();
            return lstSize;
        }

        [HttpGet]
        [Route("GetProductDetail")]
        public ProductDetail GetProductDetail(long productDetailID)
        {
            ProductDetail productDetail = db.ProductDetails.Find(productDetailID);
            
            return productDetail;
        }
    }
}
