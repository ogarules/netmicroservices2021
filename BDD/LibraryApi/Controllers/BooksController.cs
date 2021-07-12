using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Domain;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Controllers
{
    public class BooksController
    {
        private readonly ILogger<BooksController> logger;
        private readonly ILibraryService service;

        public BooksController(ILogger<BooksController> logger, ILibraryService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await this.service.GetAllBooks();
        }

        [HttpPost]
        public async Task<Book> AddBook(Book entity)
        {
            return await this.service.AddBook(entity);
        }

        [HttpPut("{id}")]
        public async Task<Book> UpdateBook(int id, Book entity) 
        {
            entity.Id = id;
            return await this.service.UpdateBook(entity);
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBook(int id)
        {
            return await this.service.GetBookById(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBook(int id)
        {
            await this.service.DeleteBook(id);
            return;
        }
    }
}