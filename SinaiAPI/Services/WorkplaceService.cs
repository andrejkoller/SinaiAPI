using Microsoft.EntityFrameworkCore;
using SinaiAPI.Data;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class WorkplaceService(SinaiDbContext context)
    {
        public IQueryable<Workplace> GetWorkplaces()
        {
            return context.Workplaces;
        }

        public List<Workplace> GetWorkplacesByDepartment(int departmentId)
        {
            return [.. context.Workplaces
                .Where(w => w.DepartmentId == departmentId)
                .Include(d => d.Department)];
        }

        public Workplace? GetWorkplace(int id)
        {
            return context.Workplaces.SingleOrDefault(x => x.Id == id);
        }

        public void PostWorkplace(Workplace workplace)
        {
            if (workplace != null)
            {
                var workplaceModel = new Workplace
                {
                    Name = workplace.Name,
                    DepartmentId = workplace.DepartmentId,
                    Status = Workplace.WorkplaceStatus.Available,
                    Active = workplace.Active,
                };

                context.Workplaces.Add(workplaceModel);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(workplace));
            }
        }

        public bool DeleteWorkplace(int id)
        {
            var workplace = context.Workplaces.SingleOrDefault(x => x.Id == id)
                ?? throw new KeyNotFoundException("Department not found");

            context.Remove(workplace);
            context.SaveChanges();

            return true;
        }

        public void UpdateWorkplace(int id, Workplace updateWorkplace)
        {
            var workplace = context.Workplaces.SingleOrDefault(x => x.Id == id)
                ?? throw new KeyNotFoundException($"Department with ID {id} not found.");

            workplace.Name = updateWorkplace.Name;
            workplace.DepartmentId = updateWorkplace.DepartmentId;
            workplace.Status = updateWorkplace.Status;
            workplace.Active = updateWorkplace.Active;

            context.SaveChanges();
        }
    }
}
