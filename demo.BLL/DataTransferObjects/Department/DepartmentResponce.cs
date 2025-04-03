namespace demo.BLL.DataTransferObjects.Department
{
    public class DepartmentResponce
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }
}
