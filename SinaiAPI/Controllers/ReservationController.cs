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
    public class ReservationController : BaseController
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService, UserService userService) : base(userService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var reservations = _reservationService.GetReservations()
                .Include(r => r.Workplace)
                .ThenInclude(d => d.Department)
                .Include(u => u.User);

            return reservations == null ? NotFound() : Ok(reservations);
        }

        [HttpGet("currentUser")]
        public IActionResult GetCurrentUser()
        {
            var reservations = _reservationService.GetCurrentUserReservations()
                .Include(w => w.Workplace)
                .ThenInclude(d => d.Department)
                .Where(u => u.UserId == CurrentUser.Id);

            return reservations == null ? NotFound() : Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var reservation = _reservationService.GetReservation(id);
            return reservation == null ? NotFound() : Ok(reservation);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }
            _reservationService.PostReservation(reservation);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _reservationService.DeleteReservation(id);

            if (!isDeleted)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("put/{id}")]
        public IActionResult Update(int id, [FromBody] Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }
            _reservationService.UpdateReservation(id, reservation);
            return Ok(reservation);
        }
    }
}
