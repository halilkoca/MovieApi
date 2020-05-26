using App.Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Core.Extensions
{
    //public static class ServiceCollectionExtensions
    //{
    //    public static void AddDependencyResolvers(this IServiceCollection services, IConfiguration configuration, ICoreModule[] modules)
    //    {
    //        foreach (var module in modules)
    //        {
    //            module.Load(services, configuration);
    //        }
    //    }
    //}
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, IConfiguration configuration, ICoreModule[] modules)
        {
            foreach (var coreModule in modules)
            {
                coreModule.Load(services, configuration);
            }

            return ServiceTool.Create(services);
        }
    }
}
