using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppBook.DataAccess.Repository.IRepository;
using WebAppBook.Models;

namespace WebAppBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProp: "Category").ToList();
			return View(productList);
		}

		public IActionResult Details(int productId)
		{
			Product product = _unitOfWork.Product.Get(u => u.Id == productId, includeProp: "Category");
			return View(product);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
