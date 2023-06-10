using LearnHubRestaurent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LearnHubRestaurent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CName = HttpContext.Session.GetString("CName");
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult CategoryProduct(int id)
        {
            var products = _context.Products.Where(p => p.CategoryId == id).ToList();
            return View(products);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SearchByCategory(string categoryName)
        {
            // Perform the search logic based on the category name
            var categories = _context.Categories.Where(c => c.CategoryName.Contains(categoryName)).ToList();

            return View("Index", categories);
        }

    }
}