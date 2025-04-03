using demo.DAL.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace demo.BLL.DataTransferObjects.Employee
{
    public class EmployeeUpdateRequest
    {
        [Required]
        public int id {  get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(5, ErrorMessage = "Min length should be 5 charachers")]
        public string Name { get; set; } = string.Empty;

        [Range(22, 30)]
        public int? Age { get; set; }
        //[RegularExpression("^\\d+\\s[\\w\\s.,#-]+$\r\n", ErrorMessage = " Address format must be like '23 ElMoghazy St.'")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public Decimal? Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool isActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender gender { get; set; }
        public EmployeeType employeeType { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId {  get; set; }
    }
}
