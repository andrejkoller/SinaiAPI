using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuideController : Controller
    {
        private readonly GuideService _guideService;

        public GuideController(GuideService guideService)
        {
            _guideService = guideService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var guides = _guideService.GetGuides();
            return guides == null ? NotFound() : Ok(guides);
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
    }
}
