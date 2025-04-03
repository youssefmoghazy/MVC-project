using demo.BLL.DataTransferObjects.Department;
using demo.DAL.Models;
using demo.DAL.Repositories;

namespace demo.BLL.Services
{
    public class DepartmentServices(IUnitOfWork unitOfWork) : IDepartmentServices
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        //private readonly IGenericRepository<Department> unitOfWork.DepartmentRepository = repository;

        public IEnumerable<DepartmentResponce> GetAll()
        {
            var departments = unitOfWork.DepartmentRepository.GetAll();

            return departments.Select(d => new DepartmentResponce
            {
                Code = d.Code,
                CreatedOn = d.CreatedOn,
                Description = d.Description,
                Id = d.Id,
                Name = d.Name
            });
        }
        public DepartmentDetailsResponce? GetById(int id)
        {
            var department = unitOfWork.DepartmentRepository.getById(id);
            // Manual Mapping
            return department is null ? null : new()
            {
                Id = department.Id,
                Name = department.Name,
                CreatedOn = department.CreatedOn,
                Description = department.Description,
                Code = department.Code,
                CreatedBy = department.CreatedBy,
                IsDeleted = department.IsDeleted,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn
            };
        }
        public int Add(DepartmentRequest request)
        {
            unitOfWork.DepartmentRepository.Add(request.ToEntity());
            return unitOfWork.SaveChanges();
        }
        public int Update(DepartmentUpateRequest request)
        {
            unitOfWork.DepartmentRepository.Update(request.ToEntity());
            return unitOfWork.SaveChanges();
        }
        public int Delete(int id)
        {
            var department = unitOfWork.DepartmentRepository.getById(id);
            if (department is null) 
                return 0;
             unitOfWork.DepartmentRepository.Delete(department);
            return unitOfWork.SaveChanges();
        }

    }
}
