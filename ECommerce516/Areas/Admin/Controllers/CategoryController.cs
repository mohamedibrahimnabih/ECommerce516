using ECommerce516.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce516.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var categories = _context.Categories/*.OrderByDescending(e=>e.Status)*/;

            // Add Filters

            return View(categories.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            TempData["success-notification"] = "Add Category Successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage, controllerName: SD.HomeController);
                //return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Update(category);
            _context.SaveChanges();

            TempData["success-notification"] = "Update Category Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage, controllerName: SD.HomeController);

            _context.Categories.Remove(category);
            _context.SaveChanges();

            TempData["success-notification"] = "Delete Category Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
