using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("AdminImage")]
    public class AdminImageController : Controller
	{
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ShoeWebsiteLtwContext db;
		public AdminImageController(ShoeWebsiteLtwContext db, IWebHostEnvironment webHostEnvironment)
		{
			this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }
		[Route("ImageTable")]
		public IActionResult Index()
		{
			var lstImage = db.Images;
			return View(lstImage);
		}

        [Route("AddImage")]
        [HttpGet]
        public IActionResult AddImage()
        {
            
            return View();
        }

        [Route("AddImage")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddImage(Image img)
        {
            if (ModelState.IsValid)
            {
                
                img.FileName = UploadFile(img);
                db.Images.Add(img);
                db.SaveChanges();
                TempData["success"] = "Add image successfully";
                return RedirectToAction("Index");
            }
            return View(img);
        }
        private string UploadFile(Image img)
        {
            string fileName = null;
            if (img.formFileImage != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "ProductImages");
                //fileName = Guid.NewGuid().ToString() + " - " + account.formFileImage.FileName;
                fileName = img.formFileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    img.formFileImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
