using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppBook.DataAccess.Context;
using WebAppBook.DataAccess.Repository.IRepository;

namespace WebAppBook.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDbContext _db;
		internal DbSet<T> dbSet;
        public Repository(AppDbContext db)
        {
			_db = db;
			this.dbSet = _db.Set<T>();
			_db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }
		
        public void Add(T entity)
		{
			_db.Add(entity);
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProp = null)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProp))
			{
				foreach (var include_prop in includeProp
					.Split(new char[] { ',' },
					StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(include_prop);
				}
			}
			return query.FirstOrDefault();
		}

		//Category, CoverType
		public IEnumerable<T> GetAll(string? includeProp = null)
		{
			IQueryable<T> query = dbSet;
			if(!string.IsNullOrEmpty(includeProp))
			{
				foreach(var include_prop in includeProp
					.Split(new char[] {','}, 
					StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(include_prop);
				}
			}
            return query.ToList();
		}
	}
}
