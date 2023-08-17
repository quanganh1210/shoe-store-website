using ThietKeWebBTL_User2.Models;

namespace ThietKeWebBTL_User2.Repository
{
	public class MenCategoryRepository: IMenCategoryRepository
	{
		private readonly ShoeWebsiteLtwContext db;

		public MenCategoryRepository(ShoeWebsiteLtwContext _db)
		{
			db = _db;
		}

		public List<Category> GetMenCategory()
		{
			List<Category> lst = (from c in db.Categories
								  join cg in db.GenderCategories on c.CategoryId equals cg.CategoryId
								  join g in db.Genders on cg.GenderId equals g.GenderId
								  where g.Name.Equals("Men")
								  select c).ToList();
			return lst;
			
		}
	}
}
