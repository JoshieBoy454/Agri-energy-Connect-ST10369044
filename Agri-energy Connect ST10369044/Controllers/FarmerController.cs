using System.Security.Claims;
using Agri_energy_Connect_ST10369044.Data;
using Agri_energy_Connect_ST10369044.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agri_energy_Connect_ST10369044.Controllers
{
    
    public class FarmerController : Controller
    {

        private readonly AppDbContext _db;
        public FarmerController(AppDbContext db) => _db = db;

        [HttpGet]
        public IActionResult FarmersHome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewProducts()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var products = _db.Products.Where(p => p.UserID == userId).ToList();
            return View(products);
        }

        [Authorize(Roles = "Farmer")]
        [HttpGet]
        public IActionResult AddProducts()
        {
            ViewBag.Categories = new List<SelectListItem> {
                new("Fruit","Fruit"),
                new("Vegetable","Vegetable"),
                new("Flower","Flower"),
                new("Seed","Seed"),
                new("Equipment","Equipment"),
                new("Other","Other")
            };
            return View();
        }

        [Authorize(Roles = "Farmer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProducts(AddProductsViewModel apvm)
        {
            if(!ModelState.IsValid) 
                return View(apvm);

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            byte[]? pPictureData = null;
            string? pPictureFileName = null;
            string? pPictureMimeType = null;

            if (apvm.pPicture is { Length: > 0 })
            {
                using var ms = new MemoryStream();
                await apvm.pPicture.CopyToAsync(ms);
                pPictureData = ms.ToArray();
                pPictureFileName = apvm.pPicture.FileName;
                pPictureMimeType = apvm.pPicture.ContentType;
            }

            var product = new Products
            {
                pName = apvm.pName,
                pCategory = apvm.pCategory,
                pProductionDate = apvm.pProductionDate,
                UserID = userId,
                pPictureData = pPictureData,
                pPictureFileName = pPictureFileName,
                pPictureMimeType = pPictureMimeType
            };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("FarmersHome", "Farmer");
        }

        
    }
}
