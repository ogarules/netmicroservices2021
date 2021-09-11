using Microsoft.EntityFrameworkCore;

namespace Department.Data
{
    public class DepartmentContext : DbContext
    {
        public DbSet<Department.Domain.Department> Department { get; set; }
        public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department.Domain.Department>();
        }
    }
}