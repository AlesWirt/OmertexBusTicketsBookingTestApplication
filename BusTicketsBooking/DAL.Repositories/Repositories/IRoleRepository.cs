using DomainModel.Models;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<Role> GetRoleByNameAsync(string roleName);

        public Task<IReadOnlyCollection<User>> GetUsersInRoleAsync(int roleId);
    }
}
