using Hospitl_Mangement_MVC.Models;

namespace Hospitl_Mangement_MVC.Interface
{
    public interface IGenericRepository<T> : IDisposable 
    {

        List<T> GetAll();
        T GetById(int id);
        int Add(T entity);
        int Update(T entity);
        void Delete(int id);

    }
}
