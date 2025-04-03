using System.ComponentModel.DataAnnotations;

namespace demo.BLL.DataTransferObjects.Department
{
    public class DepartmentRequest
    {
        [Required(ErrorMessage = "Name is required !!")]
        [MinLength(5,ErrorMessage ="min length is 5")]
        public string Name { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
