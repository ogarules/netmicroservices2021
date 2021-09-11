using System.Net.Http;
using System.Threading.Tasks;
using Employee.Models;
using Refit;

namespace Employee.Service
{
    public class DepartmentService
    {
        private readonly HttpClient httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Department> GetDepartment(int id)
        {
            var response = await this.httpClient.GetAsync($"/department/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Department>(result);
        }
    }
}