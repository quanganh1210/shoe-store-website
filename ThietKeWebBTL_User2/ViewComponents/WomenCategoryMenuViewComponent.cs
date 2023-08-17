using Microsoft.AspNetCore.Mvc;
using ThietKeWebBTL_User2.Repository;

namespace ThietKeWebBTL_User2.ViewComponents
{
    public class WomenCategoryMenuViewComponent: ViewComponent
    {
        private readonly IWomenCategoryRepository womenCategoryRepository;
        public WomenCategoryMenuViewComponent(IWomenCategoryRepository womenCategoryRepository)
        {
            this.womenCategoryRepository = womenCategoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var lst = womenCategoryRepository.GetWomenCategory();
            return View(lst);
        }
    }
}
