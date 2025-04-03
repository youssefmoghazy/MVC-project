namespace demo.DAL.Repositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }

        IDepartmentRepository DepartmentRepository { get; }

       int SaveChanges();
    }
}
