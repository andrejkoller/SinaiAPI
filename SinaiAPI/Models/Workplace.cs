using System.Text.Json.Serialization;

namespace SinaiAPI.Models
{
    public class Workplace
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WorkplaceStatus Status { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public bool Active { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum WorkplaceStatus
        {
            Available,
            Reserved,
            Blocked
        }
    }
}
