using DomainModel.Models;
using Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DAL.Repositories.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IReadOnlyCollection<string>> GetUserRoleAsync(int userId);

        public Task<IReadOnlyCollection<User>> GetAllUsersAsync();

        public Task<User> FindByNameAsync(string userName);
    }
}
