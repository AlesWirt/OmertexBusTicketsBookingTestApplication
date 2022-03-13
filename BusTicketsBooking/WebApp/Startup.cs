using Common;
using DAL.Repositories.DbContexts;
using DAL.Repositories.UnitsOfWorks;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BLL.Foundation;
using Microsoft.AspNetCore.Identity;
using DomainModel.Models;
using DAL.Repositories.Stores;

namespace WebApp
{
    public class Startup
    {
        public IConfiguration Configuration;


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BusTicketsApplicationDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
            services.AddSingleton<ILog, Logger>();
            services.AddScoped<IBookingUnitOfWork, BookingUnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserManagementService, UserManagementService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();

            services.ConfigureApplicationCookie(configure =>
            {
                configure.AccessDeniedPath = "/Home/AccessDenied";
            });

            var builder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            builder.AddRoles<Role>();
            builder.AddRoleStore<RoleStore>();
            builder.AddUserStore<UserStore>();
            builder.AddSignInManager<SignInManager<User>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
