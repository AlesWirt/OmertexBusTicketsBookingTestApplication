using Common;
using DomainModel.Models;
using DAL.Repositories.UnitsOfWorks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.Stores
{
    [UsedImplicitly]
    public class UserStore : IUserStore<User>,
        IUserPasswordStore<User>,
        IUserRoleStore<User>

    {
        private readonly ILog _logger;
        private readonly IBookingUnitOfWork _uow;


        public UserStore(ILog logger, IBookingUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }


        #region IUserStore

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("CreateAsync").Name} is null");
                throw new ArgumentNullException($"User in { typeof(UserStore).GetMethod("CreateAsync").Name} is null");
            }

            await _uow.UserRepository.CreateAsync(user);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!int.TryParse(userId, out var id))
            {
                _logger.LogError($"User id in {typeof(UserStore).GetMethod("FindByIdAsync").Name} is null");
                throw new ArgumentNullException($"User id in { typeof(UserStore).GetMethod("FindByIdAsync").Name} is null");
            }

            var user = await _uow.UserRepository.GetByIdAsync(id);

            return user;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (normalizedUserName == null)
            {
                _logger.LogError($"User normalized name in {typeof(UserStore).GetMethod("FindByNameAsync").Name} is null");
                throw new ArgumentNullException($"User normalized name in { typeof(UserStore).GetMethod("FindByNameAsync").Name} is null");
            }

            var user = await _uow.UserRepository.FindByNameAsync(normalizedUserName);

            return user;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("GetNormalizedUserNameAsync").Name} is null");
                throw new ArgumentNullException($"User in { typeof(UserStore).GetMethod("GetNormalizedUserNameAsync").Name} is null");
            }

            return Task.FromResult(user.NormalizedName);
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("GetUserIdAsync").Name} is null");
                throw new ArgumentNullException($"User in { typeof(UserStore).GetMethod("GetUserIdAsync").Name} is null");
            }

            return await Task.FromResult(user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("GetUserNameAsync").Name} is null");
                throw new ArgumentNullException($"User in { typeof(UserStore).GetMethod("GetUserNameAsync").Name} is null");
            }

            return await Task.FromResult(user.UserName);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("UpdateAsync").Name} is null");
                throw new ArgumentNullException($"User in { typeof(UserStore).GetMethod("UpdateAsync").Name} is null");
            }

            _uow.UserRepository.Update(user);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("DeleteAsync").Name} is null");
                throw new ArgumentNullException($"User in { typeof(UserStore).GetMethod("DeleteAsync").Name} is null");
            }

            _uow.UserRepository.Delete(user);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("SetNormalizedUserNameAsync").Name} is null");
                throw new ArgumentNullException($"User in { typeof(UserStore).GetMethod("SetNormalizedUserNameAsync").Name} is null");
            }

            user.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("SetUserNameAsync").Name} is null");
                throw new ArgumentNullException($"User in { typeof(UserStore).GetMethod("SetUserNameAsync").Name} is null");
            }

            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogError($"User name in {typeof(UserStore).GetMethod("SetUserNameAsync").Name} is null");
                throw new ArgumentNullException($"User name in { typeof(UserStore).GetMethod("SetUserNameAsync").Name} is null");
            }

            user.UserName = userName;

            return Task.CompletedTask;
        }

        #endregion


        #region IUserPasswordStore

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("GetPasswordHashAsync").Name} does not exist.");
                throw new ArgumentNullException($"User in {typeof(UserStore).GetMethod("GetPasswordHashAsync").Name} does not exist.");
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("HasPasswordAsync").Name} does not exist.");
                throw new ArgumentNullException($"User in {typeof(UserStore).GetMethod("HasPasswordAsync").Name} does not exist.");
            }

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("SetPasswordHashAsync").Name} does not exist.");
                throw new ArgumentNullException($"User in {typeof(UserStore).GetMethod("SetPasswordHashAsync").Name} does not exist.");
            }

            if (string.IsNullOrEmpty(passwordHash))
            {
                _logger.LogError($"Password hash in {typeof(UserStore).GetMethod("SetPasswordHashAsync").Name} does not exist.");
                throw new ArgumentNullException($"Password hash in {typeof(UserStore).GetMethod("SetPasswordHashAsync").Name} does not exist.");
            }

            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        #endregion

        #region IUserRoleStore

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("AddToRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"User in {typeof(UserStore).GetMethod("AddToRoleAsync").Name} does not exist.");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                _logger.LogError($"Role name in {typeof(UserStore).GetMethod("AddToRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role name in {typeof(UserStore).GetMethod("AddToRoleAsync").Name} does not exist.");
            }

            var role = await _uow.RoleRepository.GetRoleByNameAsync(roleName);

            if(role == null)
            {
                _logger.LogError($"Role in {typeof(UserStore).GetMethod("AddToRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role in {typeof(UserStore).GetMethod("AddToRoleAsync").Name} does not exist.");
            }

            var userRole = new UserRole
            {
                User = user,
                Role = role
            };

            await _uow.GetRepository<UserRole>().CreateAsync(userRole);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(user == null)
            {
                _logger.LogError($"Role name in {typeof(UserStore).GetMethod("GetRolesAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role name in {typeof(UserStore).GetMethod("GetRolesAsync").Name} does not exist.");
            }

            var rolesCollection = await _uow.UserRepository.GetUserRoleAsync(user.Id);

            return rolesCollection.ToList();
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(normalizedRoleName))
            {
                _logger.LogError($"Role name in {typeof(UserStore).GetMethod("GetUsersInRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role name in {typeof(UserStore).GetMethod("GetUsersInRoleAsync").Name} does not exist.");
            }

            var role = await _uow.RoleRepository.GetRoleByNameAsync(normalizedRoleName);

            if(role == null)
            {
                _logger.LogError($"Role in {typeof(UserStore).GetMethod("GetUsersInRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role in {typeof(UserStore).GetMethod("GetUsersInRoleAsync").Name} does not exist.");
            }

            var userCollection = await _uow.RoleRepository.GetUsersInRoleAsync(role.Id);

            return userCollection.ToList();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("IsInRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"User in {typeof(UserStore).GetMethod("IsInRoleAsync").Name} does not exist.");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                _logger.LogError($"Role name in {typeof(UserStore).GetMethod("IsInRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role name in {typeof(UserStore).GetMethod("IsInRoleAsync").Name} does not exist.");
            }

            var role = await _uow.RoleRepository.GetRoleByNameAsync(roleName);

            if (role == null)
            {
                _logger.LogError($"Role in {typeof(UserStore).GetMethod("IsInRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role in {typeof(UserStore).GetMethod("IsInRoleAsync").Name} does not exist.");
            }

            var userRole = await _uow.GetRepository<UserRole>().GetByIdAsync(user.Id, role.Id);

            return userRole != null;
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                _logger.LogError($"User in {typeof(UserStore).GetMethod("RemoveFromRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"User in {typeof(UserStore).GetMethod("RemoveFromRoleAsync").Name} does not exist.");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                _logger.LogError($"Role name in {typeof(UserStore).GetMethod("RemoveFromRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role name in {typeof(UserStore).GetMethod("RemoveFromRoleAsync").Name} does not exist.");
            }

            var role = await _uow.RoleRepository.GetRoleByNameAsync(roleName);

            if (role == null)
            {
                _logger.LogError($"Role in {typeof(UserStore).GetMethod("RemoveFromRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"Role in {typeof(UserStore).GetMethod("RemoveFromRoleAsync").Name} does not exist.");
            }

            var userRole = await _uow.GetRepository<UserRole>().GetByIdAsync(user.Id, role.Id);

            if(userRole == null)
            {
                _logger.LogError($"UserRole in {typeof(UserStore).GetMethod("RemoveFromRoleAsync").Name} does not exist.");
                throw new ArgumentNullException($"UserRole in {typeof(UserStore).GetMethod("RemoveFromRoleAsync").Name} does not exist.");
            }

            _uow.GetRepository<UserRole>().Delete(userRole);
            await _uow.SaveChangesAsync();
        }

        #endregion
    }
}
