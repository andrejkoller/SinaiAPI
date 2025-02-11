using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GuideController : BaseController
    {
        private readonly GuideService _guideService;

        public GuideController(GuideService guideService, UserService userService) : base(userService)
        {
            _guideService = guideService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var guides = _guideService.GetGuides();
            return guides == null ? NotFound() : Ok(guides);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var guide = _guideService.GetGuide(id);
            return guide == null ? NotFound() : Ok(guide);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Guide guide)
        {
            if (guide == null)
            {
                return BadRequest();
            }

            _guideService.PostGuide(guide);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _guideService.DeleteGuide(id);

            if (!isDeleted) {
                return NotFound($"Guide with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] Guide guide)
        {
            if (guide == null)
            {
                return BadRequest();
            }

            _guideService.UpdateGuide(id, guide);
            return Ok(guide);
        }
    }
}
