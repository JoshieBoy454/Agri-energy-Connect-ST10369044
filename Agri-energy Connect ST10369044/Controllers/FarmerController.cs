using Agri_energy_Connect_ST10369044.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agri_energy_Connect_ST10369044.Controllers
{
    [Authorize(Roles ="Farmer")]
    public class FarmerController : Controller
    {
        public IActionResult FarmersHome()
        {
            // TODO Implement logic for farmer home page
            return View();
        }

        [HttpGet]
        public IActionResult ViewProducts()
        {
            // TODO Implement logic to add product
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(AddProductsViewModel apvm)
        {
            // TODO Implement logic to add product
            return RedirectToAction("FarmersHome", "Farmer");
        }

        [HttpGet]
        public IActionResult AddProducts()
        {
            // TODO Implement logic to add product
            return View();
        }
    }
}
