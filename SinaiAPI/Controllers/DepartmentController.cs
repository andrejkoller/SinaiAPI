using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController(DepartmentService departmentService, UserService userService) : BaseController(userService)
    {
        [HttpGet]
        public IActionResult Get() 
        {
            var departments = departmentService.GetDepartments();
            return departments == null ? NotFound() : Ok(departments);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var department = departmentService.GetDepartment(id);
            return department == null ? NotFound() : Ok(department);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Department department)
        {
            if (department != null)
            {
                departmentService.PostDepartment(department);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = departmentService.DeleteDepartment(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound($"Department with ID {id} not found.");
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] Department department)
        {
            if (department != null)
            {
                departmentService.UpdateDepartment(id, department);
                return Ok(department);
            }

            return BadRequest();
        }
    }
}
