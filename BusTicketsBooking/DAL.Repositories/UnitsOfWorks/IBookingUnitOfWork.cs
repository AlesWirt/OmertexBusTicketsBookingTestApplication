using Repositories.Interfaces;
using DAL.Repositories.Repositories;

namespace DAL.Repositories.UnitsOfWorks
{
    public interface IBookingUnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get;}

        public IRoleRepository RoleRepository { get; }
    }
}
