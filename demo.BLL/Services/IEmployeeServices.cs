using demo.BLL.DataTransferObjects;
using demo.BLL.DataTransferObjects.Employee;

namespace demo.BLL.Services;

public interface IEmployeeServices
{
    int Add(EmployeeRequest request);
    int Delete(int id);
    IEnumerable<EmployeeResponce> GetAll(string? SearchValue);
    EmployeeDetailsResponce? GetById(int id);
    int Update(EmployeeUpdateRequest request);
}
