using System.Net.Http;
using System.Threading.Tasks;
using EmployeeService.Models;

namespace EmployeeService.Services
{
    public class DepartmentService
    {
        private readonly HttpClient client;
        public DepartmentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Department> GetDepartment(int id)
        {
            var response = await this.client.GetAsync($"/department/{id}");
            response.EnsureSuccessStatusCode();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            var resultString = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Department>(resultString);
        }
    }
}