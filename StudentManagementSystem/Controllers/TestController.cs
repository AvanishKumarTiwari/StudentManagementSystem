using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
