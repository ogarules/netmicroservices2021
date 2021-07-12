using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Domain;

namespace LibraryApi.Data
{
    public class LibraryDataContext : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        public LibraryDataContext(DbContextOptions<LibraryDataContext> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(r =>
            {
                r.ToTable("Authors");
                r.HasKey(s => s.Id);

                r.Property(s => s.Name).HasMaxLength(80);
            });

            modelBuilder.Entity<Book>(r =>
            {
                r.ToTable("Books");
                r.HasKey(s => s.Id);

                r.Property(s => s.Title).HasMaxLength(200);
                r.HasOne(s => s.Author).WithMany(s => s.Books).HasForeignKey(s => s.AuthorId);
            });
        }
    }
}