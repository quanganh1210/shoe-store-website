using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThietKeWebBTL_User2.Areas.Admin.ViewModels;
using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("AdminOrder")]
    public class AdminOrderController : Controller
	{
		
		private readonly ShoeWebsiteLtwContext db;
		public AdminOrderController(ShoeWebsiteLtwContext db)
		{
			this.db = db;
		}
		[Route("OrderTable")]
        public IActionResult OrderTable()
		{
			var lstOrder = db.TbOrders;
			return View(lstOrder);
		}

        [Route("EditOrder")]
        [HttpGet]
        public IActionResult EditOrder(long orderID)
        {
            TbOrder order = db.TbOrders.Find(orderID);
            return View(order);
        }
        [Route("EditOrder")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrder(TbOrder order)
        {
            if (ModelState.IsValid)
            {
                
                db.TbOrders.Update(order);
                db.SaveChanges();
                TempData["success"] = "Update order successfully";
                return RedirectToAction("OrderTable");
            }
            return View(order);
        }

        [Route("OrderDetailTable")]
        public IActionResult OrderDetailTable(long orderID)
        {
            var lstOrderDetail = db.OrderDetails.Where(x => x.OrderId == orderID);
            var lstOdVM = new List<OrderDetailViewModel>();
            foreach(var item in lstOrderDetail)
            {
                var product = (from pd in db.ProductDetails
                              join p in db.Products on pd.ProductId equals p.ProductId
                              where pd.ProductDetailId == item.ProductDetailId
                              select p).First();
                var color = (from pd in db.ProductDetails
                             join c in db.Colors on pd.ColorId equals c.ColorId
                             where pd.ProductDetailId == item.ProductDetailId
                             select c).First();
                var size = (from pd in db.ProductDetails
                             join s in db.Sizes on pd.SizeId equals s.SizeId
                             where pd.ProductDetailId == item.ProductDetailId
                             select s).First();
                var odVM = new OrderDetailViewModel
                {
                    ProductName = product.Name,
                    ColorName = color.Name,
                    SizeName = size.Name,
                    OrderDetail = item
                };
                lstOdVM.Add(odVM);
            }
            return View(lstOdVM);
        }
    }
}
