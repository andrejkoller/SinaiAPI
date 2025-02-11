using System.Text.Json.Serialization;

namespace SinaiAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public Workplace? Workplace { get; set; }
        public int WorkplaceId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReservationStatus Status { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ReservationStatus
        {
            Available,
            Reserved,
            Blocked
        }
    }
}
