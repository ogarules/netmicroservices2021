using System.Threading.Tasks;
using EmployeeService.Data;
using EmployeeService.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Services
{
    public class EmployeeService
    {
        private readonly EmployeeContext context; 
        private readonly DepartmentService departmentService;
        public EmployeeService(EmployeeContext context, DepartmentService departmentService)
        {
            this.context = context;
            this.departmentService = departmentService;
        }

        public async Task<Employee> Save(Employee data)
        {
            var entity = await this.context.Employee.FirstOrDefaultAsync(r => r.Id == data.Id);

            if (entity == null)
            {
                entity = new Employee();
                this.context.Employee.Add(entity);
            }

            entity.DepartmentId = data.DepartmentId;
            entity.Dob = data.Dob;
            entity.FamilyName = data.FamilyName;
            entity.LastName = data.LastName;
            entity.Name = data.Name;

            await this.context.SaveChangesAsync();

            return entity;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await this.context.Employee.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<object> GetAllEmployees(int page, int pageSize)
        {
            var count = await this.context.Employee.CountAsync();
            var result = await this.context.Employee.OrderBy(r => r.Name).Skip(page * pageSize).Take(pageSize).ToListAsync();

            return new
            {
                total = count,
                data = result
            };
        }

        public async Task<object> GetAllEmployeesWithDepartment(int page, int pageSize)
        {
            var count = await this.context.Employee.CountAsync();
            var result = this.context.Employee.OrderBy(r => r.Name).Skip(page * pageSize).Take(pageSize)
            .ToList()
            .Select(r => new
            {
                r.DepartmentId,
                r.Dob,
                r.FamilyName,
                r.Id,
                r.LastName,
                r.Name,
                DepartmentName = (this.departmentService.GetDepartment(r.DepartmentId).Result)?.Name
            })
            .ToList();

            return new
            {
                total = count,
                data = result
            };
        }

        public async Task<object> GetDepartmentEmployees(int page, int pageSize, int departmentId)
        {
            var query = this.context.Employee.Where(r => r.DepartmentId == departmentId);
            var count = await query.CountAsync();

            var result = await query.Skip(page * pageSize).Take(pageSize).ToListAsync();

            return new
            {
                total = count,
                data = result
            };
        }
    }
}