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

    public IActionResult Index()
    {
        var products = _context.Products.Include(e => e.Category);

        return View(products.ToList());
    }

    public IActionResult Privacy()
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
