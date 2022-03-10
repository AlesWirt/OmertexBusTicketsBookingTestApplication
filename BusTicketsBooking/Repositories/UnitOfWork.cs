using Common;
using Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly ILog _logger;
        private TContext _context;
        private bool _disposed;


        private readonly Dictionary<Type, object> _repositories;
        private readonly Dictionary<Type, Type> _registeretRepositories;


        public UnitOfWork(ILog logger, TContext context)
        {
            _logger = logger;
            _context = context;
            _repositories = new Dictionary<Type, object>();
            _registeretRepositories = new Dictionary<Type, Type>();
        }


        public async Task SaveChangesAsync()
        {
            _logger.LogInformation($"Saving changes");
            await _context.SaveChangesAsync();
        }

        public IRepository<TEntity> GetRepository<TEntity>() 
            where TEntity : class
        {
            var entityType = typeof(TEntity);

            if(_repositories.TryGetValue(entityType, out var repositoryObject))
            {
                _logger.LogInformation($"Repository of {entityType} is exsisting. Returning repository.");
                return (IRepository <TEntity>) repositoryObject;
            }


            _logger.LogInformation($"Repository of {entityType} is does not exsist. Creating repository.");
            var createdRepository = CreateRepository<TEntity>();
            _repositories.Add(entityType, createdRepository);

            return createdRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }


        protected void RegisterRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : IRepository<TEntity>
        {
            _registeretRepositories.Add(typeof(TEntity), typeof(TRepository));
        }


        private IRepository<TEntity> CreateRepository<TEntity>()
            where TEntity : class
        {
            var entityType = typeof(TEntity);

            if(!_registeretRepositories.TryGetValue(entityType, out var repositoryType))
            {
                return new Repository<TEntity>(_logger, _context);
            }

            var customRepository = Activator.CreateInstance(
                repositoryType, _logger, _context);

            return (IRepository<TEntity>)customRepository;
        }
    }
}
