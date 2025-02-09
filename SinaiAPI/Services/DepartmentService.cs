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

        public Department? GetDepartment(int id)
        {
            return _context.Departments.FirstOrDefault(x => x.Id == id);
        }

        public void PostDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            var departmentModel = new Department 
            {
                Floor = department.Floor,
                Name = department.Name,
                Amount = department.Amount,
                Description = department.Description,
                Status = Department.DepartmentStatus.Available,
                Active = true,
            };

            _context.Add(departmentModel);
            _context.SaveChanges();
        }

        public bool DeleteDepartment(int id)
        {
            var department = _context.Departments.FirstOrDefault(x => x.Id == id);

            if (department == null)
            {
                throw new KeyNotFoundException("Department not found");
            }

            _context.Remove(department);
            _context.SaveChanges();

            return true;
        }
    }
}
