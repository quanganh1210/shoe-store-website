using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Controllers
{
    public class AccessController : Controller
    {
        private const string UserSession = CommonConstants.UserSession;
        ShoeWebsiteLtwContext db;

        public AccessController(ShoeWebsiteLtwContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Login()
        {
            // Nếu người dùng chưa đăng nhập
            if (HttpContext.Session.GetString(UserSession) == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account user)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString(UserSession) == null)
                {

                    Account userInput = db.Accounts.Where(x => x.Email == user.Email & x.Password == user.Password).FirstOrDefault();
                    if (db.Accounts.Find(user.Email) == null)
                    {
                        ModelState.AddModelError("Email", "Your email doesn't exist");
                    }
                    else if (userInput == null)
                    {
                        ModelState.AddModelError("Password", "Your password is incorect");

                    }
                    else
                    {
                        if(HttpContext.Session.GetString(CommonConstants.isFromCartPage) == null)
                            HttpContext.Session.SetString(CommonConstants.isFromCartPage, "false");
                        HttpContext.Session.SetString(UserSession, JsonConvert.SerializeObject(userInput));
                        if (HttpContext.Session.GetString(CommonConstants.isFromCartPage).CompareTo("true") == 0)
                        {
                            HttpContext.Session.SetString(CommonConstants.isFromCartPage, "false");
                            return RedirectToAction("CartView", "Cart");
                        }
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove(UserSession);
            return RedirectToAction("Login", "Access");
        }

        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Account account)
        {
            if (db.Accounts.Find(account.Email) != null)
            {
                ModelState.AddModelError("Email", "Your email has been registed");
            }
            if (ModelState.IsValid)
            {
                
                db.Accounts.Add(account);
                db.SaveChanges();
                TempData["success"] = "Create account successfully";
                return RedirectToAction("Login", "Access");
            }
            return View(account);
        }
    }
}
