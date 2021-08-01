using System.Net.Http;
using System.Threading.Tasks;
using Service2.Models;

namespace Service2.Services
{
    public class ValuesService
    {
        private readonly HttpClient httpClient;

        public ValuesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ValuesResult> GetValues()
        {
            var response = await this.httpClient.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ValuesResult>(result);
        }
    }
}