namespace demo.BLL.Profile;
using AutoMapper;
using demo.BLL.DataTransferObjects.Employee;
using demo.DAL.Models;

public class EmployeeProfile : Profile
{
    public EmployeeProfile() 
    {
        CreateMap<Employee, EmployeeDetailsResponce>()
            .ForMember(e => e.Department, options => options.MapFrom(s => s.Department != null ? s.Department.Name : "Unknown"));
        CreateMap<Employee, EmployeeResponce>();

        CreateMap<EmployeeRequest, Employee>();
        CreateMap<EmployeeUpdateRequest, Employee>();
    }
}
