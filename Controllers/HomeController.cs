using Microsoft.AspNetCore.Mvc;
using MySqlTest.Models;
using System.Diagnostics;

namespace MySqlTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            TableContext _context = HttpContext.RequestServices.GetService<TableContext>() as TableContext;
            ViewBag.data = _context.GetAllInfo();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
