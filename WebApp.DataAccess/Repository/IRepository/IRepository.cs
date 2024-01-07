using System.Linq.Expressions;

namespace WebAppBook.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T - Category
		IEnumerable<T> GetAll(string? includeProp = null);
		T Get(Expression<Func<T, bool>> filter, string? includeProp = null);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
