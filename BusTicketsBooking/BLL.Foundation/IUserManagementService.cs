using DomainModel.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BLL.Foundation
{
    public interface IUserManagementService
    {
        public Task<IReadOnlyCollection<User>> GetAllUsersAsync();

        public Task<User> GetUserByUserNameAsync(string userName);

        public Task<User> GetUserByIdAsync(int userId);

        public Task<IdentityResult> UpdateUserAsync(User fromUser, User toUser);

        public Task<IdentityResult> DeleteUserAsync(User user);
    }
}