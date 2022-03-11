using DAL.Repositories.DbContexts;
using DomainModel;
using DomainModel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading.Tasks;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BusTicketsApplicationDbContext>();

                await dbContext.Database.MigrateAsync();

                var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

                await AddInitialDataAsync(userManager);
            }

            await host.RunAsync();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(
                    new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private async static Task AddInitialDataAsync(UserManager<User> userManager)
        {
            var user = new User()
            {
                UserName = "Steve",
                Email = "wirt94@mail.ru"
            };

            var identityResult = await userManager.CreateAsync(user, "123456");

            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, RoleNames.Admin);
            }
        }
    }
}
