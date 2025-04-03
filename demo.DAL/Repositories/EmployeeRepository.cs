using demo.DAL.Data.Context;

namespace demo.DAL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
