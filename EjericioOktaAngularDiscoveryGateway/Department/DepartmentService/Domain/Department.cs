namespace DepartmentService.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? OrganizationId { get; set; }
    }
}