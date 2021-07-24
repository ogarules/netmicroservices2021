using System.Collections.Generic;

namespace LibraryApi.Domain
{
    /// <summary>
    /// Autores
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Id del autor
        /// </summary>
        /// <example>111</example>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del autor
        /// </summary>
       // [Required]
        public string Name { get; set; }

        /// <summary>
        /// Libros publicados por el autor
        /// </summary>
        public List<Book> Books { get; set; } = new List<Book>();
    }
}