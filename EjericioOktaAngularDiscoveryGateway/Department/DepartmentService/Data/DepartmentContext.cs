using DepartmentService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DepartmentService.Data
{
    public class DepartmentContext : DbContext
    {
        public DbSet<Department> Department { get; set; }

        public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>();
        }
    }
}