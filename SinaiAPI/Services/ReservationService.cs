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

        public void PostReservation(int UserId, int workplaceId, Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentException(nameof(reservation));
            }

            var workplace = _context.Workplaces.SingleOrDefault(x => x.Id == workplaceId);

            if (workplace == null)
            {
                throw new KeyNotFoundException($"Workplace with {workplaceId} not found");
            }

            var user = _context.Users.SingleOrDefault(x => x.Id == UserId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with {UserId} not found");
            }

            var reservationModel = new Reservation
            {
                UserId = UserId,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
                WorkplaceId = workplaceId,
                Status = Reservation.ReservationStatus.Reserved
            };

            _context.Reservations.Add(reservationModel);
            _context.SaveChanges();
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
