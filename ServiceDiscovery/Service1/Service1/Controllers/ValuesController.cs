using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Service1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        ILogger<ValuesController> logger;
        public ValuesController(ILogger<ValuesController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<object> GetValue()
        {
            this.logger.OpenLogInformation("Obteniendo valor....");
            this.logger.OpenLogError("Error al obtener valor", new Exception("asdl-kfja ñlsdkfj añlskdjf aksdjfhlk asdhgjasdhg fjagf ajhdsg fakj"));

            return new
            {
                value = Guid.NewGuid().ToString()
            };
        }
    }
}