using System.Collections.Generic;
using System.Threading.Tasks;
using Department.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Department.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        ILogger<DepartmentController> logger;
        DepartmentService service;
        public DepartmentController(ILogger<DepartmentController> logger, DepartmentService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public async Task<object> GetAllDepartments([FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pagesize)
        {
            return await this.service.GetAllDepartments(page, pagesize);
        }

        [HttpGet("{id}")]
        public async Task<Department.Domain.Department> GetDepartment(int id)
        {
            return await this.service.GetDepartment(id);
        }

        [HttpPost]
        public async Task<Department.Domain.Department> AddDepartment(Department.Domain.Department department)
        {
            return await this.service.Save(department);
        }

        [HttpPut("{id}")]
        public async Task<Department.Domain.Department> UpdateDepartment(int id, Department.Domain.Department department)
        {
            department.Id = id;
            return await this.service.Save(department);
        }

        [HttpGet("employee")]
        public async Task<IEnumerable<object>> GetAllDepartmentsWithEmployees()
        {
            return await this.service.GetAllDepartmentsWithEmployees();
        }
    }
}