using demo.DAL.Models.Common;

namespace demo.DAL.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public virtual Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public string? Image {  get; set; }
    }
}
