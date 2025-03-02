using Microsoft.AspNetCore.SignalR;
using SinaiAPI.Models;

namespace SinaiAPI.Hubs
{
    public class ReservationHub : Hub
    {
        public async  Task SendReservationUpdate(Reservation reservation)
        {
            await Clients.All.SendAsync("ReceiveReservationUpdate", reservation);
        }
    }
}
