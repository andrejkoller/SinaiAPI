namespace SinaiAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Floor { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public DepartmentStatus Status { get; set; }
        public bool Active { get; set; }

        public enum DepartmentStatus
        {
            Available,
            Reserved,
            Blocked
        }
    }
}
