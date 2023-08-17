using Microsoft.AspNetCore.Mvc;
using ThietKeWebBTL_User2.Repository;

namespace ThietKeWebBTL_User2.ViewComponents
{
    public class MenCategoryMenuViewComponent: ViewComponent
    {
        private readonly IMenCategoryRepository menCategoryRepository;
        public MenCategoryMenuViewComponent(IMenCategoryRepository menCategoryRepository)
        {
            this.menCategoryRepository = menCategoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var lst = menCategoryRepository.GetMenCategory();
            return View(lst);
        }
    }
}
