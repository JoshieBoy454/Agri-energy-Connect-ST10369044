using Agri_energy_Connect_ST10369044.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agri_energy_Connect_ST10369044.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        public IActionResult EmployeesHome()
        {
            // TODO Implement logic for employee home page
            return View();
        }

        [HttpGet]
        public IActionResult AddFarmer()
        {
            // TODO Implement logic to add farmer
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmer(AddFarmerViewModel afvm)
        {
            // TODO Implement logic to add farmer
            return RedirectToAction("EmployeesHome","Employee");
        }

        [HttpGet]
        public IActionResult ViewFarmers()
        {
            // TODO Implement logic to view farmers
            return View();
        }
    }
}
