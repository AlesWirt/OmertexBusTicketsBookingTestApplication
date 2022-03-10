using Common;
using Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly ILog _logger;


        protected DbContext DbContext { get; }


        public Repository(ILog logger, DbContext context)
        {
            _logger = logger;
            DbContext = context;
        }



        public async Task CreateAsync(TEntity entity)
        {
            _logger.LogInformation($"Create new entity of {typeof(TEntity).Name}.");
            await DbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> GetByIdAsync(params object[] idValues)
        {
            _logger.LogDebug($"Getting entity {typeof(TEntity).Name}.");
            return await DbContext.Set<TEntity>().FindAsync(idValues);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            _logger.LogInformation($"Get all entities of the type {typeof(TEntity).Name}.");
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public void Delete(TEntity entity)
        {
            _logger.LogInformation($"Delete entity. The entity name: {typeof(TEntity).Name}.");
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _logger.LogInformation($"Update entity. The enityt name: {typeof(TEntity).Name}.");
            DbContext.Set<TEntity>().Update(entity);
        }
    }
}
