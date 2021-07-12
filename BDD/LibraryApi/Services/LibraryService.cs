using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Data;
using LibraryApi.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace LibraryApi.Services
{
    public class LibraryService : ILibraryService
    {
        LibraryDataContext context;

        ILogger<LibraryService> logger;

        public LibraryService(ILogger<LibraryService> logger, LibraryDataContext context)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Author> AddAuthor(Author entity)
        {
            logger.LogInformation($" Agregando en base de datos => {entity.Name}");

            try
            {
                Author authorDB = new Author { Name = entity.Name };
                this.context.Author.Add(authorDB);

                await this.context.SaveChangesAsync();
                return authorDB;
            }
            catch(Exception addException)
            {
                logger.LogError($"Ocurrio un error al agregar en base de datos el autor {entity.Name}", addException);
                
                throw;
            }
        }

        public async Task<Book> AddBook(Book entity)
        {
            Book bookBd = new Book { Title = entity.Title, AuthorId = entity.AuthorId };
            this.context.Book.Add(bookBd);

            await this.context.SaveChangesAsync();
            return bookBd;
        }

        public async Task DeleteAuthor(int id)
        {
            Author authorDb = await this.context.Author.FirstOrDefaultAsync(r => r.Id == id);
            if (authorDb != null)
            {
                this.context.Author.Remove(authorDb);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            Book bookDb = await this.context.Book.FirstOrDefaultAsync(r => r.Id == id);
            if (bookDb != null)
            {
                this.context.Book.Remove(bookDb);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await this.context.Author.Select(r => new Author
            {
                Name = r.Name,
                Id = r.Id
            }).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await this.context.Book.Select(r => new Book
            {
                Title = r.Title,
                AuthorId = r.AuthorId
            }).ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await this.context.Author.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Book> GetBookById(int id)
        {
            return await this.context.Book.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Author> UpdateAuthor(Author entity)
        {
            Author authorDb = await this.context.Author.FirstOrDefaultAsync(r => r.Id == entity.Id);
            if (authorDb != null)
            {
                authorDb.Name = entity.Name;
                await this.context.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<Book> UpdateBook(Book entity)
        {
            Book bookDb = await this.context.Book.FirstOrDefaultAsync(r => r.Id == entity.Id);
            if (bookDb != null)
            {
                bookDb.AuthorId = entity.AuthorId;
                bookDb.Title = entity.Title;
                await this.context.SaveChangesAsync();
            }

            return entity;
        }
    }
}