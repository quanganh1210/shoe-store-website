using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.ViewModels;

namespace ThietKeWebBTL_User2.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("AdminAccess")]
    public class AdminAccessController : Controller
    {
        private const string UserSession = CommonConstants.UserSession;
        ShoeWebsiteLtwContext db;
        public AdminAccessController(ShoeWebsiteLtwContext db)
        {
            this.db = db;
        }
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account user)
        {
            if (ModelState.IsValid)
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
                else if (userInput.RoleId != 2)
                {
                    ModelState.AddModelError("Email", "Your account isn't admin account");
                }
                else
                {
                    HttpContext.Session.SetString(UserSession, JsonConvert.SerializeObject(userInput));

                    return RedirectToAction("Index", "AdminHome");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove(UserSession);
            return RedirectToAction("Login", "AdminAccess");
        }

    }
}
