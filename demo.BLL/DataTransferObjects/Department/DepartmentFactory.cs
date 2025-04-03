namespace demo.BLL.DataTransferObjects.Department;

public static class DepartmentFactory
{

    public static DAL.Models.Department ToEntity(this DepartmentRequest request) =>
        new()
        {
            Code = request.code,
            Description = request.Description,
            Name = request.Name,
            CreatedOn = request.CreatedOn
        };
    public static DAL.Models.Department ToEntity(this DepartmentUpateRequest request) =>
        new()
        {
            Id = request.Id,
            Code = request.Code,
            Description = request.Description,
            Name = request.Name,
            CreatedOn = request.CreatedOn.ToDateTime(TimeOnly.Parse("00:00"))
        };

    public static DepartmentUpateRequest ToUpdateRequest(this DepartmentDetailsResponce department) => new()
    {
        Id = department.Id,
        Name = department.Name,
        Code = department.Code,
        CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
        Description = department.Description

    };
}
