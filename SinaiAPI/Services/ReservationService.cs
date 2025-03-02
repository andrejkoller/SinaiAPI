using Microsoft.AspNetCore.SignalR;
using SinaiAPI.Hubs;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class ReservationService
    {
        private readonly SinaiDbContext _context;
        private readonly IHubContext<ReservationHub> _hubContext;

        public ReservationService(SinaiDbContext context, IHubContext<ReservationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Reservation> GetReservations()
        {
            return _context.Reservations;
        }

        public IQueryable<Reservation> GetCurrentUserReservations()
        {
            return _context.Reservations;
        }

        public Reservation? GetReservation(int id)
        {
            return _context.Reservations.SingleOrDefault(x => x.Id == id);
        }

        public async void PostReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null");
            }

            var reservationModel = new Reservation
            {
                UserId = reservation.UserId,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
                WorkplaceId = reservation.WorkplaceId,
                Status = Reservation.ReservationStatus.Reserved
            };

            try
            {
                _context.Reservations.Add(reservationModel);
                _context.SaveChanges();
                await _hubContext.Clients.All.SendAsync("ReceiveReservationUpdate", reservation);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save the reservation", ex);
            }
        }

        public async Task<bool> DeleteReservation(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault(x => x.Id == id);

            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with {id} not found");
            }

            _context.Remove(reservation);
            _context.SaveChanges();
            await _hubContext.Clients.All.SendAsync("ReceiveReservationUpdate", reservation);

            return true;
        }

        public void UpdateReservation(int id, Reservation updateReservation)
        {
            var reservation = _context.Reservations.SingleOrDefault(x => x.Id == id);
            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with {id} not found");
            }

            reservation.StartTime = updateReservation.StartTime;
            reservation.EndTime = updateReservation.EndTime;

            _context.SaveChanges();
        }
    }
}
