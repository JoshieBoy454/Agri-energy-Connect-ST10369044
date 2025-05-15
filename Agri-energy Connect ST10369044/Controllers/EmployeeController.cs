using Agri_energy_Connect_ST10369044.Data;
using Agri_energy_Connect_ST10369044.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Agri_energy_Connect_ST10369044.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _db;
        public EmployeeController(AppDbContext db) => _db = db;

        public IActionResult EmployeesHome()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public IActionResult AddFarmers()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFarmers(AddFarmerViewModel afvm)
        {
            if (!ModelState.IsValid) return View(afvm);
            //--------------------------------------------------------------->
            //Checks if the email is unique or not
            //-------------------------------------------------------------->
            if (_db.Users.Any(u => u.Email == afvm.Email))
            {
                ModelState.AddModelError("", "Email already taken.");
                return View(afvm);
            }
            //--------------------------------------------------------------->
            //Hash's the password for privacy
            //-------------------------------------------------------------->
            var salt = Guid.NewGuid().ToString();
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: afvm.Password,
                salt: System.Text.Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            var farmer = new Users
            {
                Email = afvm.Email,
                Password = $"{salt}:{hashed}",
                Name = afvm.Name,
                Surname = afvm.Surname,
                Role = "Farmer"
            };
            _db.Users.Add(farmer);
            //--------------------------------------------------------------->
            //Signs the user in and directs them to employee add farmer page
            //-------------------------------------------------------------->
            await _db.SaveChangesAsync();
            return RedirectToAction("AddFarmers", "Employee");
        }

        [HttpGet]
        public IActionResult ViewFarmerProducts(FilterViewModel fvm)
        {
            //--------------------------------------------------->
            //Makes the list of famrmers
            //-------------------------------------------------->
            fvm.Farmers = _db.Users
                .Where(u => u.Role == "Farmer")
                .Select(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = $"{u.Name} {u.Surname}"
                })
                .ToList();

            //--------------------------------------------------->
            //Makes the list of categories from teh list of products
            //-------------------------------------------------->
            fvm.Categories = _db.Products
                .Select(p => p.pCategory)
                .Distinct()
                .Select(c => new SelectListItem
                {
                    Value = c,
                    Text = c
                })
                .ToList();

            //--------------------------------------------------->
            // Query for the products based on the filters (Category, Date, Farmer)
            //.HasValue over != null to avoid null reference exception
            //-------------------------------------------------->
            var query = _db.Products
                .Include(p => p.User)
                .AsQueryable();

            if (fvm.StartDate.HasValue)
                query = query.Where(p => p.pProductionDate >= fvm.StartDate.Value);
            if (fvm.EndDate.HasValue)
                query = query.Where(p => p.pProductionDate <= fvm.EndDate.Value);
            if (fvm.FarmerIds?.Any() == true)
                query = query.Where(p => fvm.FarmerIds.Contains(p.UserID));
            if (!string.IsNullOrEmpty(fvm.Category))
                query = query.Where(p => p.pCategory == fvm.Category);

            fvm.Products = query.ToList();
            return View(fvm);
        }
    }
}
