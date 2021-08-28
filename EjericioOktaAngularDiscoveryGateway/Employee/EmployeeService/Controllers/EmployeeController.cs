using System.Threading.Tasks;
using EmployeeService.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService.Services.EmployeeService service;

        public EmployeeController(EmployeeService.Services.EmployeeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<object> GetAllEmployeesWithDepartment([FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pageSize)
        {
            return await this.service.GetAllEmployeesWithDepartment(page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetEmployee(int id)
        {
            return await this.service.GetEmployee(id);
        }

        [HttpPost()]
        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await this.service.Save(employee);
        }

        [HttpPut("{id}")]
        public async Task<Employee> UpdateEmployee(int id, Employee employee)
        {
            employee.Id = id;
            return await this.service.Save(employee);
        }

        [HttpGet("department/{id}")]
        public async Task<object> GetDepartmentEmployees(int id, [FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pageSize = 10)
        {
            return await this.service.GetDepartmentEmployees(page, pageSize, id);
        }
        
    }
}