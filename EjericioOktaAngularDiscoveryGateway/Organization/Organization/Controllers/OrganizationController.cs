using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Organization.Domain;
using Organization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Organization.Controllers
{
    [ApiController]
    [Route("[controller]")]
   // [Authorize]
    public class OrganizationController: ControllerBase
    {
        private readonly Organization.Services.OrganizationService service;
        DepartmentService departmentService;
        EmployeeService employeeService;

        public OrganizationController(Organization.Services.OrganizationService service, DepartmentService departmentService, EmployeeService employeeService)
        {
            this.service = service;
            this.departmentService = departmentService;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<object> GetAllOrganizations([FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pageSize)
        {
            return await this.service.GetAllOrganizations(page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<Domain.Organization> GetOrganization(int id)
        {
            return await this.service.GetOrganization(id);
        }

        [HttpPost]
        public async Task<Domain.Organization> AddOrganization(Domain.Organization department)
        {
            return await this.service.Save(department);
        }

        [HttpPut("{id}")]
        public async Task<Domain.Organization> UpdateOrganization([FromRoute]int id, [FromBody] Domain.Organization department)
        {
            department.Id = id;
            return await this.service.Save(department);
        }

        [HttpGet("{id}/employee")]
        public async Task<object> GetAllDepartmentEmployees(int id)
        {
            var departments = await this.departmentService.GetOrganizationDepartments(id);

            departments.Data.ToList().ForEach(r =>
            {
                r.Employees = this.employeeService.GetEmployeesFromDepartment(r.Id).Result;
            });

            return departments;
        }
    }
}