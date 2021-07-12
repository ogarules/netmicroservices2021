using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Domain;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> logger;
        private readonly ILibraryService service;
        private readonly IBotService botService;

        public AuthorsController(ILogger<AuthorsController> logger, ILibraryService service, IBotService botService)
        {
            this.logger = logger;
            this.service = service;
            this.botService = botService;
        }

        [HttpGet]
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            logger.LogInformation("Obteniendo lista de autores");
            return await this.service.GetAllAuthors();
        }

        [HttpPost]
        public async Task<Author> AddAuthor(Author entity)
        {
            logger.LogInformation($"Agregando autor {entity.Name}");
            entity.Name = await this.botService.GetBotMessage();
            return await this.service.AddAuthor(entity);
        }

        [HttpPut("{id}")]
        public async Task<Author> UpdateAuthor(int id, Author entity) 
        {
            logger.LogInformation($"Actualizando autor {id}");
            entity.Id = id;
            return await this.service.UpdateAuthor(entity);
        }

        [HttpGet("{id}")]
        public async Task<Author> GetAuthor(int id)
        {
            logger.LogInformation($"Buscando al autor {id}");
            return await this.service.GetAuthorById(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAuthor(int id)
        {
            logger.LogInformation($"Eliminando autor {id}");
            await this.service.DeleteAuthor(id);

            logger.LogInformation($"Terminando de eliminar autor {id}");
            return;
        }
    }
}