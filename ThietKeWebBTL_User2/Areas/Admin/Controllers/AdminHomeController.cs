using Microsoft.AspNetCore.Mvc;

namespace ThietKeWebBTL_User2.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("AdminHome")]
    public class AdminHomeController : Controller
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
