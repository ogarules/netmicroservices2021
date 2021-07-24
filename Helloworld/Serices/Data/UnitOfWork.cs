using Microsoft.EntityFrameworkCore;
using Serices.Domain;

namespace Serices.Data
{
    public class UnitOfWork : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(r =>
            {
                r.ToTable("Authors");
                r.HasKey(s => s.Id);
                
            });

            modelBuilder.Entity<Book>(r =>
            {
                r.ToTable("Books");
                r.HasKey(s => s.Id);
                r.HasOne(s => s.Author).WithMany(s => s.Books).HasForeignKey(s => s.AuthorId);
            });
        }
    }
}
