using demo.DAL.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace demo.BLL.DataTransferObjects.Employee;

public class EmployeeResponce
{
    public int id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string? Address { get; set; }
    [DataType(DataType.Currency)]
    public Decimal? Salary { get; set; }
    [Display(Name = "Is Active")]
    public bool isActive { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }
    [Display(Name = "Hiring Date")]
    public Gender gender { get; set; }
    public EmployeeType employeeType { get; set; }
    public string? Department { get; set; }
    public string? Image { get; set; }

}
