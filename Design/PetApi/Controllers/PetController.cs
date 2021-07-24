using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetApi.Models;

namespace PetApi.Controllers
{
    /// <summary>
    /// Api for managing the pets
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json", "application/xml")]
    public class PetController : ControllerBase
    {
        /// <summary>
        /// Adds a new pet to the shop 
        /// </summary>
        /// <param name="pet">New pet Info</param>
        /// <returns>Added pet information</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Pet> AddPet(Pet pet)
        {
            return pet;
        }
    }
}