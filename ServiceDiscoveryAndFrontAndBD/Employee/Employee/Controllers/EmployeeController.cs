using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Employee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        ILogger<EmployeeController> logger;
        EmployeeService service;
        public EmployeeController(ILogger<EmployeeController> logger, EmployeeService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public async Task<object> GetAllEmployees([FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pagesize)
        {
            return await this.service.GetAllEmployees(page, pagesize);
        }

        [HttpGet("{id}")]
        public async Task<Employee.Domain.Employee> GetEmployee(int id)
        {
            return await this.service.GetEmployee(id);
        }

        [HttpPost]
        public async Task<Employee.Domain.Employee> AddEmployee(Employee.Domain.Employee Employee)
        {
            return await this.service.Save(Employee);
        }

        [HttpPut("{id}")]
        public async Task<Employee.Domain.Employee> UpdateEmployee(int id, Employee.Domain.Employee Employee)
        {
            Employee.Id = id;
            return await this.service.Save(Employee);
        }

        [HttpGet("department")]
        public async Task<object> GetAllEmployeesWithDepartments([FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pagesize)
        {
            return await this.service.GetAllEmployeesWithDepartment(page, pagesize);
        }

        [HttpGet("department/{id}")]
        public async Task<object> GetAllDepartmentEmployees(int id,[FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pagesize = 10)
        {
            return await this.service.GetDepartmentEmployees(id, page, pagesize);
        }
     }
}