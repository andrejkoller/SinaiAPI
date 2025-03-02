using Microsoft.EntityFrameworkCore;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class WorkplaceService
    {
        private readonly SinaiDbContext _context;

        public WorkplaceService(SinaiDbContext context)
        {
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

        public Workplace? GetWorkplace(int id)
        {
            return _context.Workplaces.SingleOrDefault(x => x.Id == id);
        }

        public void PostWorkplace(Workplace workplace)
        {
            if (workplace == null)
            {
                throw new ArgumentNullException(nameof(workplace));
            }

            var workplaceModel = new Workplace
            {
                Name = workplace.Name,
                DepartmentId = workplace.DepartmentId,
                Status = Workplace.WorkplaceStatus.Available,
                Active = workplace.Active,
            };

            _context.Workplaces.Add(workplaceModel);
            _context.SaveChanges();
        }

        public bool DeleteWorkplace(int id)
        {
            var workplace = _context.Workplaces.SingleOrDefault(x => x.Id == id);

            if (workplace == null)
            {
                throw new KeyNotFoundException("Department not found");
            }

            _context.Remove(workplace);
            _context.SaveChanges();

            return true;
        }

        public void UpdateWorkplace(int id, Workplace updateWorkplace)
        {
            var workplace = _context.Workplaces.SingleOrDefault(x => x.Id == id);

            if (workplace == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }

            workplace.Name = updateWorkplace.Name;
            workplace.DepartmentId = updateWorkplace.DepartmentId;
            workplace.Status = updateWorkplace.Status;
            workplace.Active = updateWorkplace.Active;

            _context.SaveChanges();
        }
    }
}
