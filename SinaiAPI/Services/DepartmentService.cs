using Microsoft.EntityFrameworkCore;
using SinaiAPI.Data;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class DepartmentService(SinaiDbContext context)
    {
        public IQueryable<Department> GetDepartments()
        {
            return context.Departments;
        }

        public Department? GetDepartment(int id)
        {
            return context.Departments
                .Include(d => d.Workplaces)
                .SingleOrDefault(x => x.Id == id);
        }

        public void PostDepartment(Department department)
        {
            if (department != null)
            {
                var departmentModel = new Department
                {
                    Floor = department.Floor,
                    Name = department.Name,
                    Amount = department.Amount,
                    Description = department.Description,
                    Status = department.Status,
                    Active = department.Active,
                };

                context.Add(departmentModel);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(department));
            }
        }

        public bool DeleteDepartment(int id)
        {
            var department = context.Departments.SingleOrDefault(x => x.Id == id)
                ?? throw new KeyNotFoundException("Department not found");

            context.Remove(department);
            context.SaveChanges();

            return true;
        }

        public void UpdateDepartment(int id, Department updateDepartment)
        {
            var department = context.Departments.SingleOrDefault(x => x.Id == id)
                ?? throw new KeyNotFoundException($"Department with ID {id} not found.");

            department.Floor = updateDepartment.Floor;
            department.Name = updateDepartment.Name;
            department.Amount = updateDepartment.Amount;
            department.Description = updateDepartment.Description;
            department.Status = updateDepartment.Status;
            department.Active = updateDepartment.Active;

            context.SaveChanges();
        }
    }
}
