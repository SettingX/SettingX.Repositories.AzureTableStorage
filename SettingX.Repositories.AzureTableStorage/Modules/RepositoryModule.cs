using Autofac;
using Lykke.SettingsReader.ReloadingManager;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Repositories.Blob;
using SettingX.Repositories.AzureTableStorage.Settings;

namespace SettingX.Repositories.AzureTableStorage.Modules
{
    public class RepositoryModule : Module
    {
        private readonly AppSettings _appSettings;
        
        public RepositoryModule(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            var conString = ConstantReloadingManager.From(_appSettings.ConnectionString);
            
            builder.RegisterInstance(
                new RepositoryDataRepository(new AzureBlobStorage(_appSettings.ConnectionString), "settings", "history")
            ).As<IRepositoryDataRepository>().SingleInstance();
            
            base.Load(builder);
        }
    }
}