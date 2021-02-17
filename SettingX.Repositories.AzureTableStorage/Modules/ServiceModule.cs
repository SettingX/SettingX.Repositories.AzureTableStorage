using Autofac;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.AzureTableStorage.Services;

namespace SettingX.Repositories.AzureTableStorage.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RepositoryDataService>()
                .As<IRepositoryDataService>()
                .SingleInstance();
        }
    }
}