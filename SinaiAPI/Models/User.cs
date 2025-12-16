using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SinaiAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoleType Role { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum RoleType
        {
            Admin,
            User
        }

        public ICollection<Reservation>? Reservations { get; set; } = [];
    }
}
