using ECommerce516.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce516.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class BrandController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var brands = _context.Brands/*.OrderByDescending(e=>e.Status)*/;

            // Add Filters

            return View(brands.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var brand = _context.Brands.FirstOrDefault(e => e.Id == id);

            if (brand is null)
                return RedirectToAction(SD.NotFoundPage, controllerName: SD.HomeController);
            //return NotFound();

            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.FirstOrDefault(e => e.Id == id);

            if (brand is null)
                return RedirectToAction(SD.NotFoundPage, controllerName: SD.HomeController);

            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
