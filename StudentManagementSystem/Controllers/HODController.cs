using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class HODController : Controller
    {

        public AppDb _db;
        public HODController()
        {
            _db = new AppDb();
        }
        [HttpGet]
        public IActionResult HODinfo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HODinfo(HOD Data)
        {
            // db 
            if (ModelState.IsValid) //true
            {
                _db.HOD.Add(Data);
                _db.SaveChanges();
            }
            return View();
        }
        public IActionResult GetAllHOD()
        {
            var res = _db.HOD.ToList();
            return View(res);
        }
        
    }
}