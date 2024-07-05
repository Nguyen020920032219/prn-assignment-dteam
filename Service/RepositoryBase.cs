using Repository;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public partial class RepositoryBase<T> where T : class
    {
        CarWashContext _context;
        DbSet<T> _dbSet;

        public RepositoryBase()
        {
            _context = new CarWashContext();
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
