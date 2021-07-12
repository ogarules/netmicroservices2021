using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApi.Domain;

namespace LibraryApi.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<IEnumerable<Book>> GetAllBooks();

        Task<Author> AddAuthor(Author entity);
        Task<Book> AddBook(Book entity);

        Task<Author> UpdateAuthor(Author entity);
        Task<Book> UpdateBook(Book entity);

        Task DeleteAuthor(int id);
        Task DeleteBook(int id);


        Task<Author> GetAuthorById(int id);
        Task<Book> GetBookById(int id);
    }
}