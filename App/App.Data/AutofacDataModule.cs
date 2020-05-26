using App.Core.DbTrackers;
using Autofac;

namespace App.Data
{
    public class AutofacDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

        }
    }
}
