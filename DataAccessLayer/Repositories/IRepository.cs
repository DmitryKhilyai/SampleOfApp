using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetEntitiesAsync();
        Task<T> GetByIdAsync(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        Task SaveAsync();
    }
}
