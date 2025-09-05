using ECommerce516.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce516.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class ProductController : Controller
    {
        //private ApplicationDbContext _context = new();

        private readonly IProductRepository _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Brand> _brandRepository;

        public ProductController(IProductRepository productRepository, IRepository<Category> categoryRepository, IRepository<Brand> brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAsync(includes: [e => e.Category, e => e.Brand]);

            return View(products.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAsync();
            var brands = (await _brandRepository.GetAsync()).Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = e.Name
            });

            return View(new CategoryWithBrandVM()
            {
                Categories = categories.ToList(),
                Brands = brands.ToList()
            });
        }   

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile MainImg)
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

            await _productRepository.CreateAsync(product);
            await _productRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetOneAsync(e => e.Id == id);

            if (product is null)
                return NotFound();

            var categories = await _categoryRepository.GetAsync();
            var brands = (await _brandRepository.GetAsync()).Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = e.Name
            });

            return View(new CategoryWithBrandVM()
            {
                Categories = categories.ToList(),
                Brands = brands.ToList(),
                Product = product
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile? MainImg)
        {
            var productInDb = await _productRepository.GetOneAsync(e => e.Id == product.Id, tracked: false);

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

            _productRepository.Update(product);
            await _productRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetOneAsync(e => e.Id == id);

            if (product is null)
                return NotFound();

            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", product.MainImg);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            _productRepository.Delete(product);
            await _productRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
