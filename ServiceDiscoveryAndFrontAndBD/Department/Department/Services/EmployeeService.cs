using System.Net.Http;
using System.Threading.Tasks;
using Department.Models;
using Refit;

namespace Department.Services
{
    public class EmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PageableEmployees> GetEmployeesFromDepartment(int id)
        {
            var response = await this.httpClient.GetAsync($"/employee/department/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PageableEmployees>(result);
        }
    }
}