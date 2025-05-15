using Agri_energy_Connect_ST10369044.Models;
using Agri_energy_Connect_ST10369044.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Agri_energy_Connect_ST10369044.Controllers
{
    //----------------------------------------------------------------->
    //Made with the help of chatGPT
    //Made with the help of tutorialsEU
    //---------------------------------------------------------------->
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        public AccountController(AppDbContext db) => _db = db;

        [AllowAnonymous]
        [HttpGet] 
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //=========================================================================>
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid) return View(rvm);

            //--------------------------------------------------------------->
            //Checks if the email is unique or not
            //-------------------------------------------------------------->
            if (_db.Users.Any(u => u.Email == rvm.Email))
            {
                ModelState.AddModelError("", "Email already taken.");
                return View(rvm);
            }

            //--------------------------------------------------------------->
            //Hash's the password for privacy
            //-------------------------------------------------------------->
            string salt = Guid.NewGuid().ToString();
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: rvm.Password,
              salt: System.Text.Encoding.UTF8.GetBytes(salt),
              prf: KeyDerivationPrf.HMACSHA256,
              iterationCount: 10000,
              numBytesRequested: 32));

            //---------------------------------------------------------------->
            //Creates and add's the new user into the database
            //--------------------------------------------------------------->
            var user = new Users
            {
                Password = $"{salt}:{hashed}",
                Name = rvm.Name,
                Surname = rvm.Surname,
                Email = rvm.Email,
                Role = "Employee"
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            //--------------------------------------------------------------->
            //Signs the user in and directs them to the home page
            //-------------------------------------------------------------->
            await SignInUser(user);
            return RedirectToAction("Login", "Account");
        }
        //=========================================================================>
        [AllowAnonymous]
        [HttpGet]
        //=========================================================================>
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        //=========================================================================>
        //----------------------------------------------------------------->
        // Posts the Login
        //---------------------------------------------------------------->
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //=========================================================================>
        public async Task<IActionResult> Login(LoginViewModel lvm, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(lvm);

            var user = _db.Users.SingleOrDefault(u => u.Email ==lvm.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email or password.");
                return View(lvm);
            }

            //--------------------------------------------------->
            //Checks the password to see if its correct
            //-------------------------------------------------->
            var parts = user.Password.Split(':');
            var attempt = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: lvm.Password,
            salt: System.Text.Encoding.UTF8.GetBytes(parts[0]),
            prf: KeyDerivationPrf.HMACSHA256,
              iterationCount: 10000,
              numBytesRequested: 32));

            if (attempt != parts[1])
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(lvm);
            }

            //--------------------------------------------------->
            //Logs in
            //-------------------------------------------------->
            await SignInUser(user);
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            //ADD HOME PAGE
            return RedirectToAction("Home", "Home");
        }
        //=========================================================================>

        //--------------------------------------------------->
        //Posts the logout to redirect to the login page
        //-------------------------------------------------->
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //=========================================================================>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }
        //=========================================================================>

        //--------------------------------------------------->
        // Helper for the cookies
        //-------------------------------------------------->
        //=========================================================================>
        private async Task SignInUser(Users user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                };
            var identity = new ClaimsIdentity(
              claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        //=========================================================================>
    }
}
