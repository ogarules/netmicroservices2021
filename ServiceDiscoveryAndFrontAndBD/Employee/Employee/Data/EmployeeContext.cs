using Microsoft.EntityFrameworkCore;

namespace Employee.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee.Domain.Employee> Employee { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee.Domain.Employee>();
        }
    }
}