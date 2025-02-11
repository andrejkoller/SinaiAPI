using Microsoft.AspNetCore.Mvc;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class ReservationService
    {
        private readonly SinaiDbContext _context;

        public ReservationService(SinaiDbContext context)
        {
            _context = context;
        }

        public IQueryable<Reservation> GetReservations()
        {
            return _context.Reservations;
        }

        public Reservation? GetReservation(int id)
        {
            return _context.Reservations.SingleOrDefault(x => x.Id == id);
        }

        public void PostReservation(Reservation reservation)
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
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save the reservation", ex);
            }
        }

        public bool DeleteReservation(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault(x => x.Id == id);

            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with {id} not found");
            }

            _context.Remove(reservation);
            _context.SaveChanges();

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
