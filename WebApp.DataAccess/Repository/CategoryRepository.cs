
using WebAppBook.DataAccess.Context;
using WebAppBook.DataAccess.Repository.IRepository;
using WebAppBook.Models;

namespace WebAppBook.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) : base(db)
        {
			_db = db;
        }

		public void Update(Category obj)
		{
			_db.Categories.Update(obj);
		}
	}
}
