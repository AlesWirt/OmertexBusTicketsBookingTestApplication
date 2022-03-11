using Common;
using Repositories;
using DomainModel.Models;
using DAL.Repositories.DbContexts;
using DAL.Repositories.Repositories;

namespace DAL.Repositories.UnitsOfWorks
{
    public class BookingUnitOfWork : UnitOfWork<BusTicketsApplicationDbContext>, IBookingUnitOfWork
    {
        public IUserRepository UserRepository => (IUserRepository)GetRepository<User>();

        public IRoleRepository RoleRepository => (IRoleRepository)GetRepository<Role>();


        public BookingUnitOfWork(ILog logger, BusTicketsApplicationDbContext context)
            : base(logger, context)
        {
            RegisterRepository<User, UserRepository>();
            RegisterRepository<Role, RoleRepository>();
        }
    }
}
