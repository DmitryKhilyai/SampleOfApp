using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository: IRepository<Comment>
    {
        private ApplicationContext _dbContext;

        public CommentRepository(ApplicationContext context)
        {
            _dbContext = context;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Comment>> GetEntitiesAsync()
        {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _dbContext.Comments.FindAsync(id);
        }

        public async Task CreateAsync(Comment item)
        {
            try
            {
                await _dbContext.Comments.AddAsync(item);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new ArgumentException($"The item with {item.Id} identifier already exists in the database.");
            }
        }

        public async Task UpdateAsync(Comment item)
        {
            try
            {
                _dbContext.Entry(item).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ArgumentException($"The item with {item.Id} identifier does not exist in the database.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbContext.Comments.FindAsync(id);
            if (item != null)
            {
                _dbContext.Comments.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The item with {id} identifier does not exist in the database.");
            }
        }
    }
}
