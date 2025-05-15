using Agri_energy_Connect_ST10369044.Data;
using Agri_energy_Connect_ST10369044.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ViewFarmersProducts()
        {
            // TODO Implement logic to view farmers
            return View();
        }
    }
}
