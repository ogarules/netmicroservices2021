namespace LibraryApi.Domain
{
    /// <summary>
    /// Libros
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Id del libro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titulo del libro
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Id del Autor del libro
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Autor del libro
        /// </summary>
        public Author Author { get; set; }
    }
}