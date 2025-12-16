using Microsoft.AspNetCore.SignalR;
using SinaiAPI.Data;
using SinaiAPI.Hubs;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class ReservationService(SinaiDbContext context, IHubContext<ReservationHub> hubContext)
    {
        public IQueryable<Reservation> GetReservations()
        {
            return context.Reservations;
        }

        public IQueryable<Reservation> GetCurrentUserReservations()
        {
            return context.Reservations;
        }

        public Reservation? GetReservation(int id)
        {
            return context.Reservations.SingleOrDefault(x => x.Id == id);
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
                context.Reservations.Add(reservationModel);
                context.SaveChanges();
                await hubContext.Clients.All.SendAsync("ReceiveReservationUpdate", reservation);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save the reservation", ex);
            }
        }

        public async Task<bool> DeleteReservation(int id)
        {
            var reservation = context.Reservations.SingleOrDefault(x => x.Id == id)
                ?? throw new KeyNotFoundException($"Reservation with {id} not found");

            context.Remove(reservation);
            context.SaveChanges();
            await hubContext.Clients.All.SendAsync("ReceiveReservationUpdate", reservation);

            return true;
        }

        public void UpdateReservation(int id, Reservation updateReservation)
        {
            var reservation = context.Reservations.SingleOrDefault(x => x.Id == id)
                ?? throw new KeyNotFoundException($"Reservation with {id} not found");

            reservation.StartTime = updateReservation.StartTime;
            reservation.EndTime = updateReservation.EndTime;

            context.SaveChanges();
        }
    }
}
