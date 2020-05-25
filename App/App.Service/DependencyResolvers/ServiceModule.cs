using App.Core.DbTrackers;
using App.Core.Utilities.IoC;
using App.Data;
using Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Service.DependencyResolvers
{
    public class ServiceModule : ICoreModule
    {
        public void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<InMemoryContext>();
            services.AddTransient<FileLogger>();

            RegisterProduction(services);
        }

        void RegisterProduction(IServiceCollection services)
        {
            services.AddTransient(typeof(IUserService), typeof(UserService));
        }
    }
}
