using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class Repository<T>: IRepository<T> 
        where T: class, IIdentity, new()
    {
        private ApplicationContext _dbContext;
        private DbSet<T> _dbSet = null;

        public Repository(ApplicationContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetEntitiesAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Create(T item)
        {
            _dbContext.Entry(item).State = EntityState.Added;
        }


        public void Update(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = new T { Id = id };
            _dbContext.Entry(item).State = EntityState.Deleted;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
