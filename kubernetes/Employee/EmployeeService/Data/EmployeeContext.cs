using EmployeeService.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data
{
    public class EmployeeContext :DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>();
        }
    }
}