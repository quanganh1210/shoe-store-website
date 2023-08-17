using ThietKeWebBTL_User2.Models;

namespace ThietKeWebBTL_User2.Repository
{
	public class WomenCategoryRepository: IWomenCategoryRepository
	{
		private readonly ShoeWebsiteLtwContext db;

		public WomenCategoryRepository(ShoeWebsiteLtwContext _db)
		{
			db = _db;
		}

		public List<Category> GetWomenCategory()
		{
			List<Category> lst = (from c in db.Categories
								  join cg in db.GenderCategories on c.CategoryId equals cg.CategoryId
								  join g in db.Genders on cg.GenderId equals g.GenderId
								  where g.Name.Equals("Women")
								  select c).ToList();
			return lst;
			
		}
	}
}
