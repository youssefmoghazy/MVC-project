using demo.DAL.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace demo.BLL.DataTransferObjects.Employee;

public class EmployeeDetailsResponce
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string? Address { get; set; }
    public Decimal? Salary { get; set; }
    public bool isActive { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly HiringDate { get; set; }
    public Gender gender { get; set; }
    public EmployeeType employeeType { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int LastModifiedBy { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public string? Department { get; set; }
    public string? Image { get; set; }
}
