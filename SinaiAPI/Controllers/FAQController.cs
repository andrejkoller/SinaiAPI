using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController(FAQService faqService, UserService userService) : BaseController(userService)
    {

        [HttpGet]
        public IActionResult Get()
        {
            var guides = faqService.GetGuides();
            return guides == null ? NotFound() : Ok(guides);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var faq = faqService.GetGuide(id);
            return faq == null ? NotFound() : Ok(faq);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] FAQ faq)
        {
            if (faq != null)
            {
                faqService.PostGuide(faq);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = faqService.DeleteGuide(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound($"FAQ with ID {id} not found.");
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] FAQ faq)
        {
            if (faq != null)
            {
                faqService.UpdateGuide(id, faq);
                return Ok(faq);
            }

            return BadRequest();
        }
    }
}
