using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazor_Temp.Data;
using WebAppRazor_Temp.Models;

namespace WebAppRazor_Temp.Pages.Categories
{
	[BindProperties]
    public class DeleteModel : PageModel
    {
		private readonly ApplicationDbContext _db;
		public Category Category { get; set; }

		public DeleteModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public void OnGet(int? id)
		{
			if (id != null && id != 0)
			{
				Category = _db.Categories.Find(id);
			}
		}


		public IActionResult OnPost()
		{
			Category? categoryForDelete = _db.Categories.Find(Category.Id);

			if(categoryForDelete == null)
			{
				return NotFound();
			}

			_db.Categories.Remove(categoryForDelete);
			_db.SaveChanges();
            TempData["success"] = "Category deleted succsessfully";
            return RedirectToPage("Index");
		}
	}
}
