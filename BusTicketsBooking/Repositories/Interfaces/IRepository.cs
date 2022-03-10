using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task CreateAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(params object[] idValues);

        Task<IReadOnlyCollection<TEntity>> GetAllAsync();

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}