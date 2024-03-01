using Microsoft.AspNetCore.Mvc;
using NumberToWord.Helpers;
using NumberToWord.Models;
using System.Diagnostics;

namespace NumberToWord.Controllers
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

        public JsonResult ConvertToWords(decimal number)
        {
            string outputNumbers = NumberHelper.ConvertNumberToWords(number);

            return Json(outputNumbers);
        }
    }
}
