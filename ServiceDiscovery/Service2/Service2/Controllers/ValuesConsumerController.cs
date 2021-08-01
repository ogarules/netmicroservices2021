using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service2.Services;

namespace Service2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesConsumerController
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
            this.logger.OpenLogInformation("Consumiendo Service1 ... ");
            return new
            {
                Value1 = (await this.valuesService.GetValues())
            };
        }
    }
}