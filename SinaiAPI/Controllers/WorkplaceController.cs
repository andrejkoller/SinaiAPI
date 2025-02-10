using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkplaceController : Controller
    {
        private readonly DepartmentService _departmentService;
        private readonly WorkplaceService _workplaceService;

        public WorkplaceController(DepartmentService departmentService, WorkplaceService workplaceService) {
            _departmentService = departmentService;
            _workplaceService = workplaceService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var workplaces = _workplaceService.GetWorkplaces();
            return workplaces == null ? NotFound() : Ok(workplaces);
        }

        [HttpGet("{id}")]
        public IActionResult GetBWorkplacesyDepartment(int id)
        {
            var workplaces = _workplaceService.GetWorkplacesByDepartment(id);

            if (workplaces == null || !workplaces.Any())
            {
                return NotFound($"No workplaces found for the given department ID {id}.");
            }

            return Ok(workplaces);
        }
    }
}
