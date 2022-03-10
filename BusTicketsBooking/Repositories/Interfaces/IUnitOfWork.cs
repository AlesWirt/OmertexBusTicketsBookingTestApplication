using System;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
