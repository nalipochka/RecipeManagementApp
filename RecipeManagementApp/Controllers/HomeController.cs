using Microsoft.AspNetCore.Mvc;
using RecipeManagementApp.Context;
using RecipeManagementApp.Models;
using System.Diagnostics;

namespace RecipeManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecipeManagementContext context;

        public HomeController(ILogger<HomeController> logger, RecipeManagementContext context)
        {
            _logger = logger;
            this.context = context;
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
    }
}