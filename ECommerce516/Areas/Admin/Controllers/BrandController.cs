using ECommerce516.DataAccess;
using ECommerce516.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce516.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class BrandController : Controller
    {
        private readonly IRepository<Brand> _brandRepository;

        //private ApplicationDbContext _context = new();
        //private Repository<Brand> _brandRepository = new();

        public BrandController(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepository.GetAsync();

            // Add Filters

            return View(brands.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Brand());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            //ModelState.Remove("Products");

            if(!ModelState.IsValid)
            {
                return View(brand);
            }

            await _brandRepository.CreateAsync(brand);
            await _brandRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandRepository.GetOneAsync(e => e.Id == id);

            if (brand is null)
                return RedirectToAction(SD.NotFoundPage, controllerName: SD.HomeController);
            //return NotFound();

            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }

            _brandRepository.Update(brand);
            await _brandRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandRepository.GetOneAsync(e => e.Id == id);

            if (brand is null)
                return RedirectToAction(SD.NotFoundPage, controllerName: SD.HomeController);

            _brandRepository.Delete(brand);
            await _brandRepository.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
