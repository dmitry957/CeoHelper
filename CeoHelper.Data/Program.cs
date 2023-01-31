using CeoHelper.Data.Data.Repositories;
using CeoHelper.Data.Data.Repositories.Interfaces;
using CeoHelper.Data.Entities;
using CeoHelper.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CeoHelper.Data
{
    public static class Program
    {
        public static void ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDependencies(services);
            AddDbContext(services, configuration);
            AddIdentity(services);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection(nameof(AppSettings));
            var connectionString = appSettings.GetValue<string>(nameof(AppSettings.DbConnectionString));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                connectionString,
                x =>
                {
                    x.CommandTimeout(2000);
                    x.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
                }));
        }

        private static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
        }

        private static void AddIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
