using System.Linq.Expressions;

namespace HCS.DataAccess.IRepository
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task RemoveByIdAsync(int id);
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>>? filter);
    }
}
