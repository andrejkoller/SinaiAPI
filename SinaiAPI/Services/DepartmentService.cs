using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class DepartmentService
    {
        private readonly SinaiDbContext _context;

        public DepartmentService(SinaiDbContext context)
        {
            _context = context;
        }

        public IQueryable<Department> GetDepartments()
        {
            return _context.Departments;
        }
    }
}
