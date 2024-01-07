
using WebAppBook.DataAccess.Context;
using WebAppBook.DataAccess.Repository.IRepository;

namespace WebAppBook.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ICategoryRepository Category { get; private set; }
		public IProductRepository Product { get; private set; }
		private readonly AppDbContext _db;
		public UnitOfWork(AppDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
			Product = new ProductRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
