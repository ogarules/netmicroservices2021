namespace DepartmentService.Models
{
    public class PageableEmployee
    {
        public int Total { get; set; }
        public Employee[] Data { get; set; }
    }
}