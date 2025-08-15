using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce516.Models;
using ECommerce516.ViewModels;
using ECommerce516.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ECommerce516.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ApplicationDbContext _context = new();


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(FilterVM filterVM, int page = 1)
    {
        const int discount = 50;
        var products = _context.Products.Include(e => e.Category).AsQueryable();

        // Filter
        if(filterVM.Name is not null)
        {
            products = products.Where(e => e.Name.Contains(filterVM.Name));
            ViewBag.ProductName = filterVM.Name;
        }

        if(filterVM.MinPrice is not null)
        {
            products = products.Where(e => e.Price - (e.Price * (e.Discount / 100)) >= filterVM.MinPrice);
            ViewBag.MinPrice = filterVM.MinPrice;
        }

        if (filterVM.MaxPrice is not null)
        {
            products = products.Where(e => e.Price - (e.Price * (e.Discount / 100)) <= filterVM.MaxPrice);
            ViewBag.MaxPrice = filterVM.MaxPrice;
        }

        if (filterVM.CategoryId is not null)
        {
            products = products.Where(e => e.CategoryId == filterVM.CategoryId);
            ViewBag.CategoryId = filterVM.CategoryId;
        }

        if (filterVM.IsHot)
        {
            products = products.Where(e => e.Discount >= discount);
            ViewBag.isHot = filterVM.IsHot;
        }

        // Categories
        var categories = _context.Categories;
        ViewData["categories"] = categories.ToList();
        ViewBag.categories = categories.ToList();

        // Paginitation
        var totalNumberOfPages = Math.Ceiling(products.Count() / 8.0);
        var currentPage = page;
        ViewBag.totalNumberOfPages = totalNumberOfPages;
        ViewBag.currentPage = currentPage;

        products = products.Skip((page - 1) * 8).Take(8);

        return View(products.ToList());
    }

    public IActionResult Details(int id)
    {
        var product = _context.Products.Include(e=>e.Category).FirstOrDefault(e => e.Id == id);

        if (product is null)
            return RedirectToAction(nameof(NotFoundPage));

        product.Traffic += 1;
        _context.SaveChanges();

        var relatedProducts = _context.Products.Include(e => e.Category).Where(e => e.CategoryId == product.CategoryId && e.Id != product.Id).Skip(0).Take(4);

        var topProducts = _context.Products.Include(e=>e.Category).Where(e=>e.Id != product.Id).OrderByDescending(e=>e.Traffic).Skip(0).Take(4);

        var similarProducts = _context.Products.Include(e => e.Category).Where(e => e.Name.Contains(product.Name) && e.Id != product.Id).Skip(0).Take(4);

        //var MinMaxPriceProducts = _context.Products.Include(e => e.Category).Where(e=>e.Price >= product.Price * 0.9 && e.Price <= product.Price * 1.1 && e.Id != product.Id).Skip(0).Take(4);

        return View(new ProductWithRelatedVM()
        {
            Product = product,
            RelatedProducts = relatedProducts.ToList(),
            TopProducts = topProducts.ToList(),
            SimilarProducts = similarProducts.ToList()
        });

    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult NotFoundPage()
    {
        return View();
    }


    public ViewResult Welcome()
    {
        return View();
    }

    public ViewResult PersonalInfo()
    {
        string name = "Mohamed";
        int age = 27;
        string address = "Mansoura";
        char gender = 'M';

        List<string> skills = new List<string>
        {
            "C", "C++", "C#", "SQL Server"
        };

        var PersonalInfoVM = new PersonalInfoVM()
        {
            Name = name,
            Age = age,
            Address = address,
            Gender = gender,
            Skills = skills
        };

        return View("PersonalInfomation", PersonalInfoVM);
    }

    public ViewResult PersonalInfo2()
    {
        string name = "Mohamed";
        int age = 27;
        string address = "Mansoura";
        char gender = 'M';

        List<string> skills = new List<string>
        {
            "C", "C++", "C#", "SQL Server"
        };

        var PersonalInfoVM = new PersonalInfoVM()
        {
            Name = name,
            Age = age,
            Address = address,
            Gender = gender,
            Skills = skills
        };

        return View(PersonalInfoVM);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
