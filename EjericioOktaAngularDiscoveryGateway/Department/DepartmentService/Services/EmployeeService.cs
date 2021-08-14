using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DepartmentService.Models;

namespace DepartmentService.Services
{
    public class EmployeeService
    {
        private readonly HttpClient client;

        public EmployeeService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PageableEmployee> GetEmployeesFromDepartment(int id)
        {
            var response = await this.client.GetAsync($"/employee/department/{id}");
            response.EnsureSuccessStatusCode();

            var resultStirng = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PageableEmployee>(resultStirng);
        }
    }
}