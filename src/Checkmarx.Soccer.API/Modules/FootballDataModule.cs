using System;
using Autofac;
using Checkmarx.Soccer.FootballData;
using Checkmarx.Soccer.FootballData.Interfaces;
using Checkmarx.Soccer.FootballData.Services;

namespace Checkmarx.Soccer.API.Modules
{
    public class FootballDataModule : Module
    {
        private readonly FootballDataSettings _settings;

        public FootballDataModule(FootballDataSettings settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FootballDataApi>()
                .AsSelf()
                .WithParameter("settings", _settings)
                .SingleInstance();

            builder.RegisterType<FootballDataService>()
                .As<IFootballDataService>();
        }
    }
}
