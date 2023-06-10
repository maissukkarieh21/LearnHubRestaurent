using LearnHubRestaurent.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnHubRestaurent.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["AName"] = HttpContext.Session.GetString("AName");
            ViewBag.NumberOfCustomers = _context.Customerrs.Count();
            ViewData["Sales"] = _context.Products.Sum(x => x.Sale * x.Price);
            ViewBag.NumberOfLogins = _context.UserLogins.Count();
            ViewBag.MaxPrice = _context.Products.Max(y => y.Price);
            ViewBag.MinPrice = _context.Products.Min(z => z.Price);
            var customers = _context.Customerrs.ToList();
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();
           var model = Tuple.Create<IEnumerable<Customerr>,IEnumerable<Product>,IEnumerable<Category>>(customers, products, categories);
            return View(model);
        }
        public IActionResult Order()
        {
            var customers = _context.Customerrs.ToList();
            var customerProducts = _context.ProductCustomerrs.ToList();
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();
            var model = from c in customers
                        join cp in customerProducts on c.Id equals cp.CustomerId
                        join p in products on cp.ProductId equals p.Id
                        join cat in categories on p.CategoryId equals cat.Id
                        select new Order { Customerr = c,
                            ProductCustomerr = cp, 
                            Product = p, 
                            Category = cat };
            return View(model);

        }
        private readonly ModelContext _context;
        public AdminController(ModelContext context)
        {
            _context = context;
        }
        
    }
}
