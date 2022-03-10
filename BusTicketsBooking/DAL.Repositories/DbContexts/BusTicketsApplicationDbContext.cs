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
            builder.Entity<User>(options =>
            {
                options.Property(p => p.UserName)
                    .HasMaxLength(User.UserNameMaxLength)
                    .IsRequired();
                options.Property(p => p.NormalizedName)
                    .IsRequired();
                options.Property(p => p.PasswordHash)
                    .IsRequired();
                options.Property(p => p.Email)
                    .HasMaxLength(User.UserEmailMaxLength)
                    .IsRequired();
            });

            builder.Entity<Role>(options =>
            {
                options.Property(p => p.Name)
                    .IsRequired();
                options.Property(p => p.NormalizedName)
                    .IsRequired();

                options.HasData(
                    new Role
                    {
                        Id = 1,
                        Name = RoleNames.Admin,
                        NormalizedName = RoleNames.Admin.ToUpper()
                    });

                options.HasData(
                    new Role
                    {
                        Id = 2,
                        Name = RoleNames.User,
                        NormalizedName = RoleNames.User.ToUpper()
                    });
            });

            builder.Entity<UserRole>(options =>
            {
                options.HasKey(ur => new { ur.UserId, ur.RoleId });

                options.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                options.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });
        }
    }
}
