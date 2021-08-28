using System.Net;
using System.Threading.Tasks;
using DepartmentService.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService.Services.DepartmentService service;

        public DepartmentController(DepartmentService.Services.DepartmentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<object> GetAllDepartments([FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pageSize)
        {
            return await this.service.GetAllDepartments(page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<Department> GetDepartment(int id)
        {
            return await this.service.GetDepartment(id);
        }

        [HttpPost]
        public async Task<Department> AddDepartment(Department department)
        {
            return await this.service.Save(department);
        }

        [HttpPut("{id}")]
        public async Task<Department> UpdateDepartment([FromRoute]int id, [FromBody] Department department)
        {
            department.Id = id;
            return await this.service.Save(department);
        }

        [HttpGet("{id}/employee")]
        public async Task<object> GetAllDepartmentEmployees(int id)
        {
            return await this.service.GetDepartmentEmployees(id);
        }

        [HttpGet("organization/{id}")]
        public async Task<object> GetOrganizationDepartments(int id, [FromQuery(Name = "page")] int page, [FromQuery(Name = "pagesize")] int pageSize = 10)
        {
            return await this.service.GetOrganizationDepartments(page, pageSize, id);
        }
    } 
}