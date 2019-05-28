using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private ApplicationContext _dbContext;

        public PostRepository(ApplicationContext context)
        {
            _dbContext = context;
        }

        public Task CreateAsync(Post item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbContext.Posts.FindAsync(id);
            if (item != null)
            {
                _dbContext.Posts.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The item with {id} identifier does not exist in the database.");
            }
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _dbContext.Posts.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Post>> GetEntitiesAsync()
        {
            return await _dbContext.Posts.Include(x => x.Comments).ToListAsync();
        }

        public Task UpdateAsync(Post item)
        {
            throw new NotImplementedException();
        }
    }
}
