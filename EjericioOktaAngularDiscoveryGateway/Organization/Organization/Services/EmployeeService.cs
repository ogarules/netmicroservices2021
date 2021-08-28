using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Organization.Models;

namespace Organization.Services
{
    public class EmployeeService
    {
        private readonly HttpClient client;

        public EmployeeService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PageableModel<Employee>> GetEmployeesFromDepartment(int id)
        {
            var response = await this.client.GetAsync($"/employee/department/{id}");
            response.EnsureSuccessStatusCode();

            var resultStirng = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PageableModel<Employee>>(resultStirng);
        }
    }
}