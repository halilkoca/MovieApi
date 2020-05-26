using System;
using Microsoft.Extensions.DependencyInjection;

namespace App.Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IServiceCollection Create(IServiceCollection serviceCollection)
        {
            ServiceProvider = serviceCollection.BuildServiceProvider();
            return serviceCollection;
        }
    }
}
