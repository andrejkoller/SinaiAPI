using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService userService) : BaseController(userService)
    {
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await GetCurrentUser();

            if (user != null)
            {
                return Ok(user);
            }

            return Unauthorized("User not found or not authenticated.");
        }

        [HttpGet("users")]
        public IActionResult Get()
        {
            var users = userService.GetUsers();
            return users == null ? NotFound() : Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var user = userService.GetUser(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] User user)
        {
            if (user != null)
            {
                userService.PostUser(user);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = userService.DeleteUser(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound($"User with Id {id} not found.");
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (user != null)
            {
                userService.UpdateUser(id, user);
                return Ok();
            }

            return BadRequest();
        }
    }
}
