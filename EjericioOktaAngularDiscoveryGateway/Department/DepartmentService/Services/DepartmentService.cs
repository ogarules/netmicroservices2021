using System.Reflection;
using System.Threading.Tasks;
using DepartmentService.Data;
using DepartmentService.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DepartmentService.Services
{
    public class DepartmentService
    {
        private readonly DepartmentContext context;
        private readonly EmployeeService employeeService;
        public DepartmentService(DepartmentContext context, EmployeeService employeeService)
        {
            this.context = context;
            this.employeeService = employeeService;
        }

        public async Task<Department> Save(Department data)
        {
            var entity = await this.context.Department.FirstOrDefaultAsync(r => data.Id == r.Id);

            if (entity == null)
            {
                entity = new Department();
                this.context.Add(entity);
            }

            entity.Name = data.Name;

            await this.context.SaveChangesAsync();

            return entity;
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await this.context.Department.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<object> GetAllDepartments(int page, int pageSize)
        {
            var total = await this.context.Department.CountAsync();
            var data = await this.context.Department.Skip(page * pageSize).Take(pageSize).ToListAsync();

            return new
            {
                total = total,
                data = data
            };
        }

        public async Task<object> GetDepartmentEmployees(int id)
        {
            var department = await this.context.Department.FirstOrDefaultAsync(r => r.Id == id);
            if (department == null)
            {
                return null;
            }

            return new
            {
                department.Id,
                department.Name,
                Employees = this.employeeService.GetEmployeesFromDepartment(id).Result
            };
        }
    }
}