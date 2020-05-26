using App.Core.Utilities.Interceptors;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace App.Service.DependencyResolvers
{
    public class AutofacCoreModule : Module
    {
        private readonly IConfiguration configuration;

        public AutofacCoreModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {

            var serviceList = System.Reflection.Assembly.GetEntryAssembly()?.GetReferencedAssemblies()
                    .Select(System.Reflection.Assembly.Load)
                    .SelectMany(x => x.DefinedTypes)
                    .Where(i => i.Namespace != null && i.Namespace.Contains("App.Service.Pack") && i.Name.EndsWith("Service"));
            if (serviceList != null)
                foreach (var itr in serviceList.Where(x => x.IsClass))
                {
                    var tClass = itr.AsType();
                    if (tClass != null)
                    {
                        builder.RegisterType(tClass)
                            .As(tClass.GetInterfaces().FirstOrDefault(x => x.Name == itr.Name.Insert(0, "I")))
                            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                            {
                                Selector = new AspectInterceptorSelector()
                            })
                            .InstancePerLifetimeScope();
                    }
                }

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            })
            .SingleInstance();
        }
    }
}
