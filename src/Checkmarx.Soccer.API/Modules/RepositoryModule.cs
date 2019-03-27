using System;
using Autofac;
using Checkmarx.Soccer.Domain.Interfaces;
using Checkmarx.Soccer.Infrastructure.Data;

namespace Checkmarx.Soccer.API.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IAsyncRepository<>))
                .InstancePerDependency();
        }
    }
}
