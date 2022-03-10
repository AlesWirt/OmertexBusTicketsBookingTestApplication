using DomainModel;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.DbContexts
{
    public class BusTicketsApplicationDbContext : DbContext
    {
        public BusTicketsApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
