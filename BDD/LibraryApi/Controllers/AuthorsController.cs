using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Domain;
using LibraryApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Controllers
{
    /// <summary>
    /// Api de autores
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
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

        /// <summary>
        /// Otiene los autores
        /// </summary>
        /// <returns>Lista de autores</returns>
        [HttpGet]
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            logger.LogInformation("Obteniendo lista de autores");
            return await this.service.GetAllAuthors();
        }
        
        /// <summary>
        /// Agrega un autor
        /// </summary>
        /// <param name="entity">Informacion del autor</param>
        /// <returns>Autor agregado</returns>
        /// <response code="200">Regresa el autor creado</response>
        /// <response code="400">No <see langword="sealed"/> envio la informacion correcta</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Author> AddAuthor(Author entity)
        {
            logger.LogInformation($"Agregando autor {entity.Name}");
            entity.Name = await this.botService.GetBotMessage();
            return await this.service.AddAuthor(entity);
        }

        /// <summary>
        /// Actualiza un autor
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        /// <param name="entity">Informacion del autor</param>
        /// <returns>Autor actualizado</returns>
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