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

        public IActionResult Index(string message)
        {
            TableContext _context = HttpContext.RequestServices.GetService<TableContext>();
            ViewBag.data = _context.GetAllInfo();
            ViewBag.msg = message;
            return View();
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult create(string name)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Table info = new Table() { Name = name };
                    TableContext _context = HttpContext.RequestServices.GetService<TableContext>();
                    bool rs = _context.InsertRow(info);
                    if (rs) return RedirectToAction("Index", new { message = "row added succesfullty" });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { message = "Something went wrong" });
                }
            }
            return RedirectToAction("Index", new { message = "Information put is not valid" });

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
