using System.Threading.Tasks;
using Organization.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Organization.Services
{
    public class OrganizationService
    {
        private readonly OrganizationContext context;
        private readonly EmployeeService employeeService;
        public OrganizationService(OrganizationContext context, EmployeeService employeeService)
        {
            this.context = context;
            this.employeeService = employeeService;
        }

        public async Task<Organization.Domain.Organization> Save(Organization.Domain.Organization data)
        {
            var entity = await this.context.Organization.FirstOrDefaultAsync(r => data.Id == r.Id);

            if (entity == null)
            {
                entity = new Organization.Domain.Organization();
                this.context.Add(entity);
            }

            entity.Name = data.Name;

            await this.context.SaveChangesAsync();

            return entity;
        }

        public async Task<Organization.Domain.Organization> GetOrganization(int id)
        {
            return await this.context.Organization.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<object> GetAllOrganizations(int page, int pageSize)
        {
            var total = await this.context.Organization.CountAsync();
            var data = await this.context.Organization.Skip(page * pageSize).Take(pageSize).ToListAsync();

            return new
            {
                total = total,
                data = data
            };
        }
    }
}