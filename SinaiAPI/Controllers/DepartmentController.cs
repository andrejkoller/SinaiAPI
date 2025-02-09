using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;
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
        public IActionResult Get() 
        {
            var departments = _departmentService.GetDepartments();
            return departments == null ? NotFound() : Ok(departments);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var departmentId = _departmentService.GetDepartment(id);
            return departmentId == null ? NotFound() : Ok(departmentId);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Department department)
        {
            if (department == null) 
            { 
                return BadRequest();
            }

            _departmentService.PostDepartment(department);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        { 
            bool isDeleted = _departmentService.DeleteDepartment(id);

            if (!isDeleted) 
            {
                return NotFound($"Department with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
