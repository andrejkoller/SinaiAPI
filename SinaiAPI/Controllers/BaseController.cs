using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;
using SinaiAPI.Services;
using System.Security.Claims;

namespace SinaiAPI.Controllers
{
    public abstract class BaseController(UserService userService) : Controller
    {
        protected User? CurrentUser
        {
            get
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userId, out int id))
                {
                    return null;
                }

                return userService.GetUser(id);
            }
        }
    }

}
