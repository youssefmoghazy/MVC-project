using System.Collections.Specialized;
using AutoMapper;
using demo.BLL.DataTransferObjects.Department;
using demo.BLL.DataTransferObjects.Employee;
using demo.DAL.Data.Context;
using demo.DAL.Models;
using demo.DAL.Repositories;
using demo.BLL.Services.AttachmentSevices;

namespace demo.BLL.Services;

public class EmployeeServices(IUnitOfWork unitOfWork , IMapper mapper, AttachmentSevices.IAttachmentServices attachmentSevices ) : IEmployeeServices
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    //private readonly IGenericRepository<Employee> unitOfWork.EmployeeRepository = resposity;
    private readonly IMapper _mapper = mapper;
    private readonly AttachmentSevices.IAttachmentServices _attachmentSevices = attachmentSevices;

    public IEnumerable<EmployeeResponce> GetAll(string? searchValue)
    {
        if (string.IsNullOrWhiteSpace(searchValue))
        {
            return unitOfWork.EmployeeRepository.GetAll<EmployeeResponce>(e => new EmployeeResponce
            {
                Address = e.Address,
                Age = e.Age,
                Email = e.Email,
                employeeType = e.EmployeeType,
                gender = e.gender,
                id = e.Id,
                isActive = e.IsActive,
                Name = e.Name,
                PhoneNumber = e.PhoneNumber,
                Salary = e.Salary,
                Department = e.Department != null ? e.Department.Name : "Unknown",
                Image = e.Image

            }, e => !e.IsDeleted ,
            e => e.Department);
        }
        var employees = unitOfWork.EmployeeRepository.GetAll<EmployeeResponce>( e=>  new EmployeeResponce
        {
            Address = e.Address,
            Age = e.Age,
            Email = e.Email,
            employeeType = e.EmployeeType,
            gender = e.gender,
            id = e.Id,
            isActive = e.IsActive,
            Name = e.Name,
            PhoneNumber = e.PhoneNumber,
            Salary = e.Salary,
            Department = e.Department != null ? e.Department.Name : "Unknown",
            Image = e.Image

        },e => !e.IsDeleted && e.Name.ToLower().Contains(searchValue.ToLower()),
        e => e.Department);
        return employees;
    }

    public EmployeeDetailsResponce? GetById(int id)
    {
        var Employee = unitOfWork.EmployeeRepository.getById(id);
        // using automapper Mapping
        return Employee is null ? null : _mapper.Map<EmployeeDetailsResponce>(Employee);
    }
    public int Add(EmployeeRequest request)
    {
        unitOfWork.EmployeeRepository.Add(_mapper.Map<Employee>(request));
        if (request.Image is not null)
            _attachmentSevices.Upload(request.Image, "images");
        return unitOfWork.SaveChanges();
    }

    public int Update(EmployeeUpdateRequest request)
    {
        unitOfWork.EmployeeRepository.Update(_mapper.Map<Employee>(request));
        return unitOfWork.SaveChanges();
    }
    public int Delete(int id)
    {
        var employee = unitOfWork.EmployeeRepository.getById(id);
        if (employee == null) 
            return 0;
        employee.IsDeleted = true;
        unitOfWork.EmployeeRepository.Update(employee);
        return unitOfWork.SaveChanges();
    }
}
