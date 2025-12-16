using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinaiAPI.Models;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkplaceController(WorkplaceService workplaceService, UserService userService) : BaseController(userService)
    {
        [HttpGet]
        public IActionResult Get()
        {
            var workplaces = workplaceService.GetWorkplaces()
                .Include(d => d.Department);

            return workplaces == null ? NotFound() : Ok(workplaces);
        }

        [HttpGet("department/{id}")]
        public IActionResult GetBWorkplacesyDepartment(int id)
        {
            var workplaces = workplaceService.GetWorkplacesByDepartment(id);

            if (workplaces != null && workplaces.Count != 0)
            {
                return Ok(workplaces);
            }

            return NotFound($"No workplaces found for the given department ID {id}.");
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var workplace = workplaceService.GetWorkplace(id);
            return workplace == null ? NotFound() : Ok(workplace);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Workplace workplace)
        {
            if (workplace != null)
            {
                workplaceService.PostWorkplace(workplace);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = workplaceService.DeleteWorkplace(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound($"Department with ID {id} not found.");
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] Workplace workplace)
        {
            if (workplace != null)
            {
                workplaceService.UpdateWorkplace(id, workplace);
                return Ok(workplace);
            }

            return BadRequest();
        }
    }
}
