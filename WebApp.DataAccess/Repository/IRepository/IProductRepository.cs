
using WebAppBook.Models;

namespace WebAppBook.DataAccess.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		void Update(Product obj);
	}
}
