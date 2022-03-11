using Common;
using Repositories;
using DomainModel.Models;
using DAL.Repositories.DbContexts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ILog logger, BusTicketsApplicationDbContext context)
            : base(logger, context)
        {

        }


        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            var role = await DbContext.Set<Role>()
                .SingleOrDefaultAsync(r => r.Name == roleName);

            return role;
        }

        public async Task<IReadOnlyCollection<User>> GetUsersInRoleAsync(int roleId)
        {
            var users = await DbContext.Set<UserRole>()
                .Where(userRole => userRole.RoleId == roleId)
                .Select(userRole => userRole.User)
                .ToListAsync();

            return users;
        }
    }
}
