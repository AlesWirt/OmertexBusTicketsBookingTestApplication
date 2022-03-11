using Common;
using DomainModel.Models;
using DAL.Repositories.DbContexts;
using Repositories;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using JetBrains.Annotations;

namespace DAL.Repositories.Repositories
{
    [UsedImplicitly]
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ILog logger, BusTicketsApplicationDbContext context)
            : base(logger, context)
        {

        }


        public async Task<IReadOnlyCollection<string>> GetUserRoleAsync(int userId)
        {
            var roleNameCollection = await DbContext.Set<UserRole>()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            return roleNameCollection;
        }

        public async Task<IReadOnlyCollection<User>> GetAllUsersAsync()
        {
            var collection = await DbContext.Set<User>()
                .Include(u => u.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            var user = await DbContext.Set<User>()
                .SingleOrDefaultAsync(u => u.UserName == userName);

            return user;
        }
    }
}
