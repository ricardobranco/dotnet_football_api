using System;
using Autofac;
using Checkmarx.Soccer.API.Interfaces;
using Checkmarx.Soccer.API.Services;

namespace Checkmarx.Soccer.API.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompetitionService>()
                .As<ICompetitionService>();
        }
    }
}
