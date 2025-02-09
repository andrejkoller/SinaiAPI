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
            var department = _departmentService.GetDepartment(id);
            return department == null ? NotFound() : Ok(department);
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

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        { 
            bool isDeleted = _departmentService.DeleteDepartment(id);

            if (!isDeleted) 
            {
                return NotFound($"Department with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }

            _departmentService.UpdateDepartment(id, department);
            return Ok(department);
        }
    }
}
