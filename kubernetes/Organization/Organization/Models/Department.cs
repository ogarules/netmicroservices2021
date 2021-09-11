namespace Organization.Models
{
    public class Department
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }

        public PageableModel<Employee> Employees { get; set; }

    }
}