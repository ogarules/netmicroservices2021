using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service2.Services;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Service2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ValuesConsumerController : ControllerBase
    {
        ILogger<ValuesConsumerController> logger; 
        IValuesSerice ivaluesService; 
        ValuesService valuesService;
        public ValuesConsumerController(ILogger<ValuesConsumerController> logger, IValuesSerice ivaluesService, ValuesService valuesService)
        {
            this.logger = logger;
            this.ivaluesService = ivaluesService;
            this.valuesService = valuesService;
        }

        [HttpGet]
        public async Task<object> GetFromService1()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

            this.logger.OpenLogInformation("Consumiendo Service1 ... ");
            return new
            {
                Value1 = (await this.valuesService.GetValues()),
                Value2 = (await this.ivaluesService.GetValues())
            };
        }
    }
}