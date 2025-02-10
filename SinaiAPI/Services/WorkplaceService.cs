using Microsoft.EntityFrameworkCore;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class WorkplaceService
    {
        private readonly SinaiDbContext _context;

        public WorkplaceService(SinaiDbContext context) {
            _context = context;
        }

        public IQueryable<Workplace> GetWorkplaces()
        {
            return _context.Workplaces;
        }

        public List<Workplace> GetWorkplacesByDepartment(int departmentId)
        {
            return _context.Workplaces
                .Where(w => w.DepartmentId == departmentId)
                .Include(d => d.Department)
                .ToList();
        }
    }
}
