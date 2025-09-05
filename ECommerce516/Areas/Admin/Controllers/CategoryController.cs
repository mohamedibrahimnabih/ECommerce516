using ECommerce516.DataAccess;
using ECommerce516.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce516.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class CategoryController : Controller
    {
        //private ApplicationDbContext _context = new();
        //private Repository<Category> _categoryRepository = new();

        private IRepository<Category> _categoryRepository;// = new Repository<Category>();

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAsync();

            // Add Filters

            return View(categories.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Add Category Successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetOneAsync(e => e.Id == id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage, controllerName: SD.HomeController);
                //return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _categoryRepository.Update(category);
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Update Category Successfully";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetOneAsync(e => e.Id == id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage, controllerName: SD.HomeController);

            _categoryRepository.Delete(category);
            await _categoryRepository.CommitAsync();

            TempData["success-notification"] = "Delete Category Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
