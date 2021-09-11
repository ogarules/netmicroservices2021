using System.Threading.Tasks;
using Department.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Department.Services
{
    public class DepartmentService
    {
        DepartmentContext context;
        EmployeeService employeeService;
        public DepartmentService(DepartmentContext context, EmployeeService employeeService)
        {
            this.context = context;
            this.employeeService = employeeService;
        }

        public async Task<Department.Domain.Department> Save(Department.Domain.Department department)
        {
            Department.Domain.Department dbEntity = await this.context.Department.FirstOrDefaultAsync(r => r.Id == department.Id);
            if (dbEntity == null)
            {
                dbEntity = new Domain.Department();
                this.context.Department.Add(dbEntity);
            }

            dbEntity.Name = department.Name;

            await this.context.SaveChangesAsync();
            return dbEntity;
        }

        public async Task<object> GetAllDepartments(int page, int pagesize)
        {
            page = page == 0 ? 0 : page -1;
            var count = this.context.Department.Count();
            var result = await this.context.Department.OrderBy(r => r.Name).Skip(page*pagesize).Take(pagesize).ToListAsync();
            return new
            {
                total = count,
                data = result
            };
        }

        public async Task<Domain.Department> GetDepartment(int id)
        {
            return await this.context.Department.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<object>> GetAllDepartmentsWithEmployees()
        {
            var m = await this.employeeService.GetEmployeesFromDepartment(1);
            var result = await this.context.Department.ToListAsync();

            return result.Select(r => new
            {
                r.Name,
                r.Id,
                Employees = this.employeeService.GetEmployeesFromDepartment(r.Id).Result
            });
        }

    }
}