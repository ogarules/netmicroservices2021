namespace Department.Models
{
    public class PageableEmployees
    {
        public int Total { get; set; }
        public Employee[] Data { get; set; }
    }
}