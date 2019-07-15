using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Defines a set of methods for a Unit of Work.
    /// </summary>
    /// <typeparam name="TDbContext">The type of context used to perform CRUD on a database.</typeparam>
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext, new()
    {
        private readonly TDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class that uses the specified DbContext to access stored information.
        /// </summary>
        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Retrieves a repository.
        /// </summary>
        /// <typeparam name="T">The type of repository to be retrieved.</typeparam>
        /// <returns>A repository.</returns>
        /// <example>
        /// <code>
        /// IRepository&lt;User&gt; userRepository = unitOfWork.GetRepository&lt;User&gt;();
        /// User user = userRepository.GetById(2);
        /// </code>
        /// </example>
        public IRepository<T> GetRepository<T>() where T : class, IIdentity, new()
        {
            if (_repositories.Keys.Contains(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }

            var repository = new Repository<T>(_dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        /// <summary>
        /// Persists any changes to the underlying data.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the context has been disposed.</exception>
        /// <example>
        /// <code>
        /// IRepository&lt;User&gt; userRepository = unitOfWork.GetRepository&lt;User&gt;();
        /// User user = userRepository.GetById(2);
        /// user.Name = "Updated Name";
        /// userRepository.Update(user);
        /// unitOfWork.Save();
        /// </code>
        /// </example>
        public async Task<int> SaveAsync()
        {
            //try
            //{
            return await _dbContext.SaveChangesAsync();
            //}
            //catch (ValidationException ex)
            //{

            //}
        }

        /// <summary>
        /// Returns new transaction for current context. Method is used to manage transaction commiting manually.
        /// </summary>
        /// <returns>Db Transaction.</returns>
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void RollbackChanges()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}