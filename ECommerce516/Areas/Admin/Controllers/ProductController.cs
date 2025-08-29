using ECommerce516.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce516.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class ProductController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var products = _context.Products.Include(e => e.Category).Include(e => e.Brand);

            return View(products.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories;
            var brands = _context.Brands;

            return View(new CategoryWithBrandVM()
            {
                Categories = categories.ToList(),
                Brands = brands.ToList()
            });
        }   

        [HttpPost]
        public IActionResult Create(Product product, IFormFile MainImg)
        {
            if (MainImg is null)
                return BadRequest();

            if (MainImg.Length > 0)
            {
                // Save img in wwwroot
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName);
                // djsl-kds232-91321d-sadas-dasd213213.png
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    MainImg.CopyTo(stream);
                }

                // Save img in DB
                product.MainImg = fileName;
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id);

            if (product is null)
                return NotFound();

            var categories = _context.Categories;
            var brands = _context.Brands;

            return View(new CategoryWithBrandVM()
            {
                Categories = categories.ToList(),
                Brands = brands.ToList(),
                Product = product
            });
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile? MainImg)
        {
            var productInDb = _context.Products.AsNoTracking().FirstOrDefault(e => e.Id == product.Id);

            if (productInDb is null)
                return NotFound();

            if(MainImg is not null)
            {
                // Save img in wwwroot
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName);
                // djsl-kds232-91321d-sadas-dasd213213.png
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    MainImg.CopyTo(stream);
                }

                // Remove old Img from wwwroot
                var oldFileName = productInDb.MainImg;
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", oldFileName);
                if(System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                // Save img in DB
                product.MainImg = fileName;
            }
            else
            {
                product.MainImg = productInDb.MainImg;
            }

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id);

            if (product is null)
                return NotFound();

            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", product.MainImg);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
