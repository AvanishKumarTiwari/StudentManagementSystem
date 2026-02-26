using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [StudentManagementSystem.Filters.DemoAuthorize]
    public class AdminController : Controller
    {

        private readonly AppDb _db;
        public AdminController(AppDb db)
        {
            _db = db;
        }

        // Very small demo auth helper (based on demo cookie set at login)
        private bool IsAuthenticated()
        {
            var cookies = Request?.Cookies;
            if (cookies == null) return false;
            var role = cookies["AuthRole"];
            return !string.IsNullOrEmpty(role);
        }
        [HttpGet]
        public IActionResult CreateUser()  
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User Data)
        {
            // db 
            if (ModelState.IsValid) //true
            {
                _db.Users.Add(Data);
                _db.SaveChanges();    
            }
            return View();
        }

        public IActionResult GetAllUsers() {
           var res = _db.Users.ToList();
            return View(res);
        }

        public IActionResult AdminLogin() {
            return View();
        }

        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminRegister(string fullname, string email, string phone, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Please fill required fields.");
                return View();
            }

            if (password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View();
            }

            // TODO: persist admin record (demo only)

            return RedirectToAction("AdminLogin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminLogin(string role, string email, string password)
        {
            // Very simple placeholder authentication - replace with real auth.
            if (string.IsNullOrEmpty(role))
            {
                ModelState.AddModelError(string.Empty, "Please select a role.");
                return View();
            }

            // For demo: accept any non-empty email/password
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View();
            }

            if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // set a simple auth cookie for demo purposes
                Response.Cookies.Append("AuthRole", "Admin", new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTimeOffset.UtcNow.AddHours(8)
                });

                return RedirectToAction("AdminDashboard");
            }
            else if (role.Equals("HOD", StringComparison.OrdinalIgnoreCase))
            {
                Response.Cookies.Append("AuthRole", "HOD", new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTimeOffset.UtcNow.AddHours(8)
                });

                return RedirectToAction("Studentdashboard");
            }

            // default
            return RedirectToAction("AdminLogin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // remove demo auth cookie
            if (Request.Cookies.ContainsKey("AuthRole"))
            {
                Response.Cookies.Delete("AuthRole");
            }

            return RedirectToAction("AdminLogin");
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }
        public IActionResult Studentdashboard()
        {
            return View();
        }

        public IActionResult Products() 
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }
    }
}
