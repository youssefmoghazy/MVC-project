using demo.DAL.Data.Context;

namespace demo.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        //private readonly Lazy<IEmployeeRepository> _employeeRepository;
        //private readonly Lazy<IDepartmentRepository> _departmentRepository;

        private readonly Func<IEmployeeRepository> _employeerepositoryfactory;
        private readonly Func<IDepartmentRepository> _departmentrepositoryfactory;
        public UnitOfWork(ApplicationDBContext context,
            Func<IEmployeeRepository> employeerepositoryfactory,
            Func<IDepartmentRepository> departmentrepositoryfactory )
        {
            _context = context;
            ///_employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
            ///_departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(context));
            _employeerepositoryfactory = employeerepositoryfactory;
            _departmentrepositoryfactory = departmentrepositoryfactory;
        }
        private IEmployeeRepository _employeerepository;
        private IDepartmentRepository _departmentrepository;
        public IEmployeeRepository EmployeeRepository => _employeerepository ??= _employeerepositoryfactory.Invoke();

        public IDepartmentRepository DepartmentRepository => _departmentrepository ??= _departmentrepositoryfactory.Invoke();

        public int SaveChanges() => _context.SaveChanges();
    }
}
