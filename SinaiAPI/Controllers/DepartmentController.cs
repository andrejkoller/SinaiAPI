using Microsoft.AspNetCore.Mvc;

namespace SinaiAPI.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
