using Autofac;
using AutoMapper;
using Lykke.Common.Log;
using Lykke.SettingsReader.ReloadingManager;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Repositories.Blob;
using SettingX.Repositories.AzureTableStorage.Repositories.KeyValue;
using SettingX.Repositories.AzureTableStorage.Repositories.Lock;
using SettingX.Repositories.AzureTableStorage.Repositories.Repository;
using SettingX.Repositories.AzureTableStorage.Repositories.Token;
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
            
            builder
                .RegisterInstance(
                    new RepositoryDataRepository(new AzureBlobStorage(_appSettings.ConnectionString), "settings", "history"))
                .As<IRepositoryDataRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new AccountTokenHistoryRepository(
                        AzureStorage.Tables.AzureTableStorage<AccountTokenHistoryEntity>.Create(conString, "AccessTokenHistory", c.Resolve<ILogFactory>())))
                .As<IAccountTokenHistoryRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new ConnectionUrlHistoryRepository(
                        AzureStorage.Tables.AzureTableStorage<ConnectionUrlHistoricEntity>.Create(conString, "ConnectionUrlHistory", c.Resolve<ILogFactory>()),
                        c.Resolve<IMapper>()))
                .As<IConnectionUrlHistoricEventsRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new KeyValueHistoryRepository(
                        AzureStorage.Tables.AzureTableStorage<KeyValueHistoricEntity>.Create(conString, "KeyValueHistory", c.Resolve<ILogFactory>()),
                        new AzureBlobStorage(conString.CurrentValue), "keyvaluehistory"))
                .As<IKeyValueHistoryRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new EditLockRepository(AzureStorage.Tables.AzureTableStorage<EditLockEntity>.Create(conString, "Lock", c.Resolve<ILogFactory>()), c.Resolve<IMapper>()))
                .As<IEditLockRepository>()
                .SingleInstance();
            
            base.Load(builder);
        }
    }
}