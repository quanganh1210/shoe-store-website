using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Web.Helpers;
using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Controllers
{
    public class AccountController : Controller
	{
        private const string UserSession = CommonConstants.UserSession;
        private readonly ShoeWebsiteLtwContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(ShoeWebsiteLtwContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
		{
            Account user = new Account();
            if (HttpContext.Session.GetString(UserSession) != null)
                user = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(UserSession));
            
            return View(user);
		}
        [HttpGet]
        public IActionResult EditProfile()
        {
            Account user = new Account();
            if (HttpContext.Session.GetString(UserSession) != null)
                user = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(UserSession));
            else
                return RedirectToAction("Index");
            return View(user);
        }
        [HttpPost]
        public IActionResult EditProfile(Account accountUpdated)
        {
            if(ModelState.IsValid)
            {
                string fileName = UploadFile(accountUpdated);
                accountUpdated.Image = fileName;
                HttpContext.Session.SetString(UserSession, JsonConvert.SerializeObject(accountUpdated));
                db.Accounts.Update(accountUpdated);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountUpdated);
        }
        private string UploadFile(Account account)
        {
            string fileName = null;
            if(account.formFileImage != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "AccountImages");
                fileName = Guid.NewGuid().ToString() + " - " + account.formFileImage.FileName;
                //fileName = account.formFileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    account.formFileImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        [HttpPost]
        public JsonResult CreateAccount(string email, string name, string strBirthday)
        {
            // MM/dd/yyyy
            Account account = new Account();
            account = db.Accounts.Find(email);
            if (account == null)
            {
                DateTime birthday = DateTime.ParseExact(strBirthday, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                account = new Account
                {
                    Email = email,
                    Name = name,
                    Password = "123",
                    DateOfBirth = birthday,
                    RoleId = 1,
                    Status = 1
                };
                db.Accounts.Add(account);
                db.SaveChanges();
            }
            HttpContext.Session.SetString(UserSession, JsonConvert.SerializeObject(account));
            return Json(new
            {
                status = true
            });
        }
    }
}
