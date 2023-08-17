using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = CommonConstants.CartSession;
        private const string UserSession = CommonConstants.UserSession;
        private readonly ShoeWebsiteLtwContext db;
        public CartController(ShoeWebsiteLtwContext db)
        {
            
            this.db = db;
        }
        [HttpGet]
        public IActionResult CartView()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CartView(TbOrder order)
        {
            if (ModelState.IsValid)
            {
                if(HttpContext.Session.GetString(UserSession) == null)
                {
                    //CommonConstants.fromCartPage = true;
                    HttpContext.Session.SetString(CommonConstants.isFromCartPage, "true");
                    return RedirectToAction("Login", "Access");
                }
                else
                {
                    long orderID;
                    var lastOrder = db.TbOrders.OrderByDescending(x => x.OrderId).FirstOrDefault();
                    if(lastOrder == null)
                    {
                        orderID = 1;
                    }
                    else
                    {
                        orderID = lastOrder.OrderId + 1;
                    }
                    
                    decimal? totalPrice = 0;
                    var lstCartItem = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
                    var user = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(UserSession));
                    for (int i = 0;i < lstCartItem.Count();++i)
                    {
                        Decimal? currentPrice = getCurrentPrice(lstCartItem[i]);
                        totalPrice += currentPrice * lstCartItem[i].Quantity;
                    }
                    order.OrderId = orderID;
                    order.TotalPrice = totalPrice;
                    order.Status = "Waitting";
                    order.Email = user.Email;
                    db.TbOrders.Add(order);
                    db.SaveChanges();
                    foreach(var item in lstCartItem)
                    {
                        OrderDetail detail = new OrderDetail();
                        detail.OrderId = orderID;
                        detail.Quantity = item.Quantity;
                        detail.UnitSellingPrice = getCurrentPrice(item);
                        detail.ProductDetailId = item.ProductDetailID;
                        db.OrderDetails.Add(detail);
                        db.SaveChanges();
                    }
                    var lst = new List<CartItem>();
                    HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(lst));
                    return RedirectToAction("OrderCompleted", "Cart");
                }
            }
            return View();
        }

        Decimal? getCurrentPrice(CartItem cartItem)
        {
            Decimal? currentPrice;
            if (cartItem.ProductDetail.Discount == 0)
                currentPrice = cartItem.ProductDetail.UnitSellingPrice;
            else
                currentPrice = cartItem.ProductDetail.UnitSellingPrice * (Decimal)(1 - cartItem.ProductDetail.Discount / 100);
            return currentPrice;
        }

        public IActionResult OrderCompleted()
        {
            return View();
        }

        
        [HttpGet]
        public JsonResult GetAll()
        {
            if(HttpContext.Session.GetString(CartSession) == null)
            {
                var lst = new List<CartItem>();
                HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(lst));
            }
            var lstCartItem = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));

            //return Json(lstCartItem);
            return Json(new
            {
                data = lstCartItem,
                status = true
            });
        }
        [HttpPost]
        public JsonResult Add(long productDetailID, long productID, int quantity)
        {
            var lstCartItem = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));

            if(lstCartItem.Any(x => x.ProductDetail.ProductDetailId == productDetailID))
            {
                foreach (var item in lstCartItem)
                {
                    if (item.ProductDetailID == productDetailID)
                    {
                        item.Quantity += quantity;
                        break;
                    }    
                }     
            }
            else
            {
                CartItem newItem = new CartItem();
                newItem.ProductDetailID = productDetailID;
                newItem.ProductID = productID;
                newItem.ProductDetail = db.ProductDetails.Find(productDetailID);
                newItem.Product = db.Products.Find(productID);
                var size = db.Sizes.Find(newItem.ProductDetail.SizeId);
                newItem.SizeName = size.Name;
                newItem.Quantity = quantity;
                lstCartItem.Add(newItem);
            }
            HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(lstCartItem));
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var lstCartItem = JsonConvert.DeserializeObject<List<CartItem>>(cartData);
            var cartSession = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
            foreach(var item in cartSession)
            {
                foreach(var jitem in lstCartItem)
                {
                    if(item.ProductDetail.ProductDetailId == jitem.ProductDetailID)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }
            HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(cartSession));
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(new List<CartItem>()));
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(long productDetailID)
        {
            var lstCartItem = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
            if (lstCartItem != null)
            {
                lstCartItem.RemoveAll(x => x.ProductDetail.ProductDetailId == productDetailID);
                HttpContext.Session.SetString(CartSession, JsonConvert.SerializeObject(lstCartItem));
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        
        public JsonResult SumCartItemQuantity()
        {
            var lstCartItem = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString(CartSession));
            int totalQuantity = 0;
            foreach(var item in lstCartItem)
            {
                totalQuantity += item.Quantity;
            }
            return Json(new
            {
                totalQuantity = totalQuantity
            });
        }
    }
}
