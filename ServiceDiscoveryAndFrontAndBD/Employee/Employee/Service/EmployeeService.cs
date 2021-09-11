using System.Globalization;
using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Employee.Data;
using Employee.Service;
using Microsoft.EntityFrameworkCore;

namespace Employee
{
    public class EmployeeService
    {
        EmployeeContext context;
        DepartmentService departmentService;
        public EmployeeService(EmployeeContext context, DepartmentService departmentService)
        {
            this.context = context;
            this.departmentService = departmentService;
        }

        public async Task<Employee.Domain.Employee> Save(Employee.Domain.Employee employee)
        {
            Employee.Domain.Employee dbEntity = await this.context.Employee.FirstOrDefaultAsync(r => r.Id == employee.Id);
            if (dbEntity == null)
            {
                dbEntity = new Domain.Employee();
                this.context.Employee.Add(dbEntity);
            }

            dbEntity.DepartmentId = employee.DepartmentId;
            dbEntity.Dob = employee.Dob;
            dbEntity.FamilyName = employee.FamilyName;
            dbEntity.LastName = employee.LastName;
            dbEntity.Name = employee.Name;

            await this.context.SaveChangesAsync();

            return dbEntity;
        }

        public async Task<object> GetAllEmployees(int page, int pagesize)
        {
            page = page == 0 ? 0 : page -1;
            var count = this.context.Employee.Count();
            var result = await this.context.Employee.OrderBy(r => r.Name).Skip(page*pagesize).Take(pagesize).ToListAsync();
            return new
            {
                total = count,
                data = result
            };
        }

        public async Task<Domain.Employee> GetEmployee(int id)
        {
            return await this.context.Employee.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<object> GetAllEmployeesWithDepartment(int page, int pagesize)
        {
            page = page == 0 ? 0 : page -1;
            //var rr = (await this.departmentService.GetDepartment(1));

            var count = this.context.Employee.Count();

            var result = this.context.Employee.Skip(page*pagesize).Take(pagesize).ToList().Select(r => new
            {
                r.DepartmentId,
                r.Dob,
                r.FamilyName,
                r.Id,
                r.LastName,
                r.Name
               , DepartmentName = (this.departmentService.GetDepartment(r.DepartmentId).Result)?.Name
            }).ToList();

            return new
            {
                total = count,
                data = result
            };
        }

        public async Task<object> GetDepartmentEmployees(int departmentId, int page, int pagesize)
        {
            page = page == 0 ? 0 : page -1;
            //var rr = (await this.departmentService.GetDepartment(1));

            var count = this.context.Employee.Count();

            var result = await this.context.Employee.Skip(page*pagesize).Take(pagesize).Where(r => r.DepartmentId == departmentId).ToListAsync();
            return new
            {
                total = count,
                data = result
            };
        }
    }
}