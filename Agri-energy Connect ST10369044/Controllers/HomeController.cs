using System.Diagnostics;
using Agri_energy_Connect_ST10369044.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agri_energy_Connect_ST10369044.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Home()
        {
            if (!User.Identity.IsAuthenticated)
                return View("Home");       
            if (User.IsInRole("Employee"))
                return RedirectToAction("EmployeesHome", "Employee");
            if (User.IsInRole("Farmer"))
                return RedirectToAction("FarmersHome", "Farmer");
            return View("Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
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
