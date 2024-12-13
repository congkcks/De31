using De3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace De3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private OnlineShopContext _context;

        public HomeController(ILogger<HomeController> logger,OnlineShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            var products= (from p in _context.Products
                          where p.Available==true && p.UnitPrice<=1000
                          select p).ToList();
            ViewBag.Products = products;
            return View();
        }
        [HttpGet]
        // tra ve patian view
        public IActionResult PhanLoai(string name)
		{
            var products = (from p in _context.Products
							where p.Category.Name == name
							select p).ToList();
            return PartialView("SanPham",products);
		}
        public IActionResult ThemSanPham()
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
    }
}
