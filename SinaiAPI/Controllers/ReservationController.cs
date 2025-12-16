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
    public class ReservationController(ReservationService reservationService, UserService userService) : BaseController(userService)
    {
        [HttpGet]
        public IActionResult Get()
        {
            var reservations = reservationService.GetReservations()
                .Include(r => r.Workplace)
                .ThenInclude(d => d.Department)
                .Include(u => u.User);

            return reservations == null ? NotFound() : Ok(reservations);
        }

        [HttpGet("currentUser")]
        public IActionResult GetCurrentUser()
        {
            var reservations = reservationService.GetCurrentUserReservations()
                .Include(w => w.Workplace)
                .ThenInclude(d => d.Department)
                .Where(u => u.UserId == CurrentUser.Id);

            return reservations == null ? NotFound() : Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var reservation = reservationService.GetReservation(id);
            return reservation == null ? NotFound() : Ok(reservation);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Reservation reservation)
        {
            if (reservation != null)
            {
                reservationService.PostReservation(reservation);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await reservationService.DeleteReservation(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound($"Reservation with ID {id} not found.");
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] Reservation reservation)
        {
            if (reservation != null)
            {
                reservationService.UpdateReservation(id, reservation);
                return Ok(reservation);
            }

            return BadRequest();
        }
    }
}
