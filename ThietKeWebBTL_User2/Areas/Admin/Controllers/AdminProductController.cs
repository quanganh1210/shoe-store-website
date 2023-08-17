using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("AdminProduct")]
    public class AdminProductController : Controller
    {
        private ShoeWebsiteLtwContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AdminProductController(ShoeWebsiteLtwContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var lstProduct = db.Products;
            return View(lstProduct);
        }

        [Route("AddProduct")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [Route("AddProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                Account admin = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(CommonConstants.UserSession));
                product.CreatedBy = admin.Email;
                db.Products.Add(product);
                db.SaveChanges();
                TempData["success"] = "Add product successfully";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Route("EditProduct")]
        [HttpGet]
        public IActionResult EditProduct(long productID)
        {
            Product product = db.Products.Find(productID);
            return View(product);
        }
        [Route("EditProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                Account admin = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(CommonConstants.UserSession));
                product.ModifiedBy = admin.Email;
                product.ModifiedDate = DateTime.Now;
                db.Products.Update(product);
                db.SaveChanges();
                TempData["success"] = "Update product successfully";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Route("DeleteProduct")]
        [HttpGet]
        public IActionResult DeleteProduct(long productID)
        {
            Account admin = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(CommonConstants.UserSession));
            var product = db.Products.Find(productID);
            product.ModifiedBy = admin.Email;
            product.ModifiedDate = DateTime.Now;
            product.Status = 0;
            db.Products.Update(product);
            db.SaveChanges();
            TempData["success"] = "Set product status to idle successfully";
            return RedirectToAction("Index");
        }

        [Route("ProductDetail")]
        [HttpGet]
        public IActionResult ProductDetail(long productID)
        {
            var product = db.Products.Find(productID);
            return View(product);
        }

        [Route("ProductDetailTable")]
        public IActionResult ProductDetailTable()
        {
            var lstProducVM = new List<ProductViewModelIdle>();
            var lstProductDetail = db.ProductDetails;
            foreach(var item in lstProductDetail)
            {
                Product product = db.Products.Find(item.ProductId);
                Size size = db.Sizes.Find(item.SizeId);
                Color color = db.Colors.Find(item.ColorId);
                ProductViewModelIdle productVM = new ProductViewModelIdle
                {
                    Product = product,
                    ProductDetail = item,
                    ColorName = color.Name,
                    SizeName = size.Name
                };
                lstProducVM.Add(productVM);
            }
            return View(lstProducVM);
        }

        [Route("AddProductDetail")]
        [HttpGet]
        public IActionResult AddProductDetail()
        {
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "Name");
            ViewBag.ColorId = new SelectList(db.Colors.ToList(), "ColorId", "Name");
            ViewBag.SizeId = new SelectList(db.Sizes.ToList(), "SizeId", "Name");
            return View();
        }

        [Route("AddProductDetail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProductDetail(ProductDetail product)
        {
            if (ModelState.IsValid)
            {
                Account admin = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(CommonConstants.UserSession));
                product.CreatedBy = admin.Email;
                product.Image = UploadFile(product);
                db.ProductDetails.Add(product);
                db.SaveChanges();
                TempData["success"] = "Add product detail successfully";
                return RedirectToAction("ProductDetailTable");
            }
            return View(product);
        }
        private string UploadFile(ProductDetail productDetail)
        {
            string fileName = null;
            if (productDetail.formFileImage != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "ProductImages");
                //fileName = Guid.NewGuid().ToString() + " - " + account.formFileImage.FileName;
                fileName = productDetail.formFileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productDetail.formFileImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        [Route("EditProductDetail")]
        [HttpGet]
        public IActionResult EditProductDetail(long productDetailID)
        {
            ProductDetail productDetail = db.ProductDetails.Find(productDetailID);
            return View(productDetail);
        }
        [Route("EditProductDetail")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProductDetail(ProductDetail product)
        {
            if (ModelState.IsValid)
            {
                Account admin = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(CommonConstants.UserSession));
                product.ModifiedBy = admin.Email;
                product.ModifiedDate = DateTime.Now;
                db.ProductDetails.Update(product);
                db.SaveChanges();
                TempData["success"] = "Update product detail successfully";
                return RedirectToAction("ProductDetailTable");
            }
            return View(product);
        }

        [Route("DeleteProductDetail")]
        [HttpGet]
        public IActionResult DeleteProductDetail(long productDetailID)
        {
            Account admin = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(CommonConstants.UserSession));
            var product = db.ProductDetails.Find(productDetailID);
            product.ModifiedBy = admin.Email;
            product.ModifiedDate = DateTime.Now;
            product.Status = 0;
            db.ProductDetails.Update(product);
            db.SaveChanges();
            TempData["success"] = "Set product detail status to idle successfully";
            return RedirectToAction("ProductDetailTable");
        }

        [Route("GenderProductTable")]
        public IActionResult GenderProductTable()
        {
            var lstGenderProductVM = new List<GenderProductViewModel>();
            var lstGenderProduct = db.GenderProducts;
            foreach(var item in lstGenderProduct)
            {
                var gender = db.Genders.Find(item.GenderId);
                var product = db.Products.Find(item.ProductId);
                var gpVM = new GenderProductViewModel
                {
                    GenderProduct = item,
                    GenderName = gender.Name,
                    ProductName = product.Name
                };
                lstGenderProductVM.Add(gpVM);
            }
            return View(lstGenderProductVM);
        }

        [Route("AddGenderProduct")]
        [HttpGet]
        public IActionResult AddGenderProduct()
        {
            ViewBag.GenderId = new SelectList(db.Genders.ToList(), "GenderId", "Name");
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "Name");
            return View();
        }

        [Route("AddGenderProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGenderProduct(GenderProduct gp)
        {
            if (ModelState.IsValid)
            {
                
                db.GenderProducts.Add(gp);
                db.SaveChanges();
                TempData["success"] = "Add GenderProduct successfully";
                return RedirectToAction("GenderProductTable");
            }
            return View(gp);
        }

        //[Route("EditGenderProduct")]
        //[HttpGet]
        //public IActionResult EditGenderProduct(long genderID, long productID)
        //{
        //    GenderProduct gp = db.GenderProducts.Where(x => x.GenderId == genderID && x.ProductId == productID).First();
        //    return View(gp);
        //}
        //[Route("EditGenderProduct")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditGenderProduct(GenderProduct gp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Account admin = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(CommonConstants.UserSession));
        //        db.GenderProducts.Update(gp);
        //        db.SaveChanges();
        //        TempData["success"] = "Update GenderProduct successfully";
        //        return RedirectToAction("GenderProductTable");
        //    }
        //    return View(gp);
        //}

        [Route("DeleteGenderProduct")]
        [HttpGet]
        public IActionResult DeleteGenderProduct(long genderID, long productID)
        {
            
            db.GenderProducts.Remove(db.GenderProducts.Where(x => x.GenderId == genderID && x.ProductId == productID).FirstOrDefault());
            db.SaveChanges();
            TempData["success"] = "Delete GenderProduct successfully";
            return RedirectToAction("GenderProductTable");
        }

        // ProductCategory
        [Route("ProductCategoryTable")]
        public IActionResult ProductCategoryTable()
        {
            var lstpcVM = new List<ProductCategoryViewModel>();
            var lstpc = db.ProductCategories;
            foreach (var item in lstpc)
            {
                var category = db.Categories.Find(item.CategoryId);
                var product = db.Products.Find(item.ProductId);
                var pcVM = new ProductCategoryViewModel
                {
                    CategoryName = category.Name,
                    ProductName = product.Name,
                    productCategory = item
                };
                lstpcVM.Add(pcVM);
            }
            return View(lstpcVM);
        }

        [Route("AddProductCategory")]
        [HttpGet]
        public IActionResult AddProductCategory()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "Name");
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "Name");
            return View();
        }

        [Route("AddProductCategory")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProductCategory(ProductCategory pc)
        {
            if (ModelState.IsValid)
            {

                db.ProductCategories.Add(pc);
                db.SaveChanges();
                TempData["success"] = "Add ProductCategory successfully";
                return RedirectToAction("ProductCategoryTable");
            }
            return View(pc);
        }

        

        [Route("DeleteProductCategory")]
        [HttpGet]
        public IActionResult DeleteProductCategory(long categoryID, long productID)
        {

            db.ProductCategories.Remove(db.ProductCategories.Where(x => x.CategoryId == categoryID && x.ProductId == productID).FirstOrDefault());
            db.SaveChanges();
            TempData["success"] = "Delete Category successfully";
            return RedirectToAction("ProductCategoryTable");
        }

        [Route("Test")]
        public IActionResult Test()
        {
            var lstProductDetail = db.Products;
            return View(lstProductDetail);
        }

    } 
}
