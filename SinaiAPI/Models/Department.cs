using System.Text.Json.Serialization;

namespace SinaiAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Floor { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DepartmentStatus Status { get; set; }
        public bool Active { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum DepartmentStatus
        {
            Available,
            Reserved,
            Blocked
        }
        public ICollection<Workplace>? Workplaces { get; set; } = [];
    }
}
