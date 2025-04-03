using demo.BLL.DataTransferObjects.Department;

namespace demo.BLL.Services
{
    public interface IDepartmentServices
    {
        int Add(DepartmentRequest request);
        int Delete(int id);
        IEnumerable<DepartmentResponce> GetAll();
        DepartmentDetailsResponce? GetById(int id);
        int Update(DepartmentUpateRequest request);
    }
}