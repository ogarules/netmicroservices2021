using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Organization.Models;

namespace Organization.Services
{
    public class DepartmentService
    {
        private readonly HttpClient client;

        public DepartmentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PageableModel<Department>> GetOrganizationDepartments(int id)
        {
            var response = await this.client.GetAsync($"/department/organization/{id}");
            response.EnsureSuccessStatusCode();

            var resultStirng = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PageableModel<Department>>(resultStirng);
        }
    }
}