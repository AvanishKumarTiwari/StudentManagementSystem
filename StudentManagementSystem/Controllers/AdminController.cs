using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class AdminController : Controller
    {

        public AppDb _db;
        public AdminController()
        {
            _db = new AppDb();
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

        
    }
}
