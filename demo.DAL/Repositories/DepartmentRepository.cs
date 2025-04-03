using demo.DAL.Data.Context;

namespace demo.DAL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>
        , IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDBContext context)
            : base(context) { 
        }
    }
}
