using Microsoft.EntityFrameworkCore;

namespace Organization.Data
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Domain.Organization> Organization { get; set; }

        public OrganizationContext(DbContextOptions<OrganizationContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Organization>();
        }
    }
}