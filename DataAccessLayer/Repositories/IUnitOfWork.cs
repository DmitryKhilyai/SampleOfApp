using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, IIdentity, new();

        Task<int> SaveAsync();

        IDbContextTransaction BeginTransaction();

        void RollbackChanges();
    }
}
