using System;

namespace Department.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FamilyName { get; set; }

        public DateTime Dob { get; set; }

        public int DepartmentId { get; set; }
    }
}