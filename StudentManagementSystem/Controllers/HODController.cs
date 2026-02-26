using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [StudentManagementSystem.Filters.DemoAuthorize]
    public class HODController : Controller
    {

        private readonly AppDb _db;
        public HODController(AppDb db)
        {
            _db = db;
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