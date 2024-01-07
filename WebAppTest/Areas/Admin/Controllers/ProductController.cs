using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppBook.DataAccess.Repository.IRepository;
using WebAppBook.Models;
using WebAppBook.Models.ViewModels;
using WebAppBook.Utility;

namespace WebAppBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        { 
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProp:"Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
			IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});

            ProductVM productVM = new ProductVM()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                //Create
				return View(productVM);
			}
			else
            {
                //Update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
				return View(productVM);
			}
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM new_product, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(new_product.Product.ImageUrl))
                    {
                        //Delete old image when update
                        var oldImagePath = Path.Combine(wwwRootPath, new_product.Product.ImageUrl.TrimStart('/'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var FileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(FileStream);
                    }

                    new_product.Product.ImageUrl = @"/images/product/" + fileName;
				}

                if(new_product.Product.Id == 0)
                {
					_unitOfWork.Product.Add(new_product.Product);
				}
                else
                {
                    _unitOfWork.Product.Update(new_product.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProp: "Category").ToList();
            return Json(new {data = objProductList});
        }

        [HttpDelete]
		public IActionResult Delete(int? id)
		{
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if(productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting"});
            }

			var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('/'));

			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });
		}
		#endregion
	}
}
