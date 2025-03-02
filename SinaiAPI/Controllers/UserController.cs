using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;
using SinaiAPI.Services;

namespace SinaiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        public UserController(UserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await GetCurrentUser();

            if (user == null)
            {
                return Unauthorized("User not found or not authenticated.");
            }

            return Ok(user);
        }

        [HttpGet("users")]
        public IActionResult Get()
        {
            var users = _userService.GetUsers();
            return users == null ? NotFound() : Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var user = _userService.GetUser(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userService.PostUser(user);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _userService.DeleteUser(id);

            if (!isDeleted)
            {
                return NotFound($"User with Id {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userService.UpdateUser(id, user);
            return Ok();
        }
    }
}
