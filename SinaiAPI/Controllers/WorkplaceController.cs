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
    public class WorkplaceController : BaseController
    {
        private readonly WorkplaceService _workplaceService;

        public WorkplaceController(WorkplaceService workplaceService, UserService userService) : base(userService) {
            _workplaceService = workplaceService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var workplaces = _workplaceService.GetWorkplaces()
                .Include(d => d.Department);

            return workplaces == null ? NotFound() : Ok(workplaces);
        }

        [HttpGet("department/{id}")]
        public IActionResult GetBWorkplacesyDepartment(int id)
        {
            var workplaces = _workplaceService.GetWorkplacesByDepartment(id);

            if (workplaces == null || !workplaces.Any())
            {
                return NotFound($"No workplaces found for the given department ID {id}.");
            }

            return Ok(workplaces);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var workplace = _workplaceService.GetWorkplace(id);
            return workplace == null ? NotFound() : Ok(workplace);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Workplace workplace)
        {
            if (workplace == null)
            {
                return BadRequest();
            }

            _workplaceService.PostWorkplace(workplace);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _workplaceService.DeleteWorkplace(id);

            if (!isDeleted)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] Workplace workplace)
        {
            if (workplace == null)
            {
                return BadRequest();
            }

            _workplaceService.UpdateWorkplace(id, workplace);
            return Ok(workplace);
        }
    }
}
