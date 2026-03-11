using Hospitl_Mangement_MVC.Data;
using Hospitl_Mangement_MVC.Interface;
using Hospitl_Mangement_MVC.Models;

namespace Hospitl_Mangement_MVC.Repository
{
    public class GenericRepositoy<T> : IGenericRepository<T> where T : class
    {
        private readonly HospitalDbContext _context;

        public GenericRepositoy(HospitalDbContext context)
        {
            _context = context;
        }
        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Set<T>().Find(id);
            _context.Remove(item);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<T> GetAll()
      => _context.Set<T>().ToList();

        public T GetById(int id)
      => _context.Set<T>().Find(id);

        public int Update(T entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();
        }
    }
}
