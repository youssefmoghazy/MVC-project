
using System.Linq.Expressions;
using demo.DAL.Data.Context;

namespace demo.DAL.Repositories
{
    public class GenericRepository<TEntity>(ApplicationDBContext context) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDBContext _context = context;

        //Get 
        public TEntity? getById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        //Get All
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
            => withTracking ? _context.Set<TEntity>().Where(d => !d.IsDeleted) :
            _context.Set<TEntity>().AsNoTracking().Where(d => !d.IsDeleted).ToList();

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> quary = _context.Set<TEntity>();
            foreach(var include in includes)
            {
                quary.Include(include);
            }
            return quary.Where(predicate)
                .AsNoTracking()
                .Select(selector)
                .ToList();
        }

        //Add
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        //Update
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        //Delete
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

    }
}
