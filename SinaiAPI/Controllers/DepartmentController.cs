using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Get() {
            var departments = _departmentService.GetDepartments();
            return departments == null ? NotFound() : Ok(departments);
        }
    }
}
