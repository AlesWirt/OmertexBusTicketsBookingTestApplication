using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DomainModel.Models;
using DAL.Repositories.UnitsOfWorks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories.Stores
{
    [UsedImplicitly]
    public class RoleStore : IRoleStore<Role>
    {

        private readonly ILog _logger;
        private readonly IBookingUnitOfWork _uow;


        public RoleStore(ILog logger, IBookingUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }


        public void Dispose()
        {
            _uow.Dispose();
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                _logger.LogError($"Role cannot be null");

                throw new ArgumentNullException($"Role cannot be null"); ;
            }

            await _uow.RoleRepository.CreateAsync(role);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                _logger.LogError($"Role does not exist");

                throw new ArgumentNullException($"Role does not exist");
            }

            _uow.RoleRepository.Update(role);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                _logger.LogError($"Role cannot be null");

                throw new ArgumentNullException($"Role cannot be null"); ;
            }

            _uow.RoleRepository.Delete(role);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!int.TryParse(roleId, out var id))
            {
                _logger.LogError("Invalid identificator value");

                throw new ArgumentNullException("Invalid identificator value");
            }

            var role = await _uow.RoleRepository.GetByIdAsync(id);

            return role;
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _uow.RoleRepository.GetRoleByNameAsync(normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                _logger.LogError($"Role does not exist");

                throw new ArgumentNullException($"Role does not exist");
            }

            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                _logger.LogError($"Role does not exist");

                throw new ArgumentNullException($"Role does not exist");
            }

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                _logger.LogError($"Role does not exist");

                throw new ArgumentNullException($"Role does not exist");
            }

            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                _logger.LogError($"Role does not exist");

                throw new ArgumentNullException($"Role does not exist");
            }

            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                _logger.LogError($"Role does not exist");

                throw new ArgumentNullException($"Role does not exist");
            }

            role.Name = roleName;

            return Task.CompletedTask;
        }
    }
}