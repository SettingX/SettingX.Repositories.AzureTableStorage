using Autofac;
using AutoMapper;
using Lykke.Common.Log;
using Lykke.SettingsReader.ReloadingManager;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Repositories.Blob;
using SettingX.Repositories.AzureTableStorage.Repositories.KeyValue;
using SettingX.Repositories.AzureTableStorage.Repositories.Lock;
using SettingX.Repositories.AzureTableStorage.Repositories.Network;
using SettingX.Repositories.AzureTableStorage.Repositories.Repository;
using SettingX.Repositories.AzureTableStorage.Repositories.ServiceToken;
using SettingX.Repositories.AzureTableStorage.Repositories.Token;
using SettingX.Repositories.AzureTableStorage.Repositories.User;
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
            var secretsConnString = ConstantReloadingManager.From(_appSettings.SecretsConnectionString);
            var userConnString = ConstantReloadingManager.From(_appSettings.UserConnectionString);
            
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
                    new KeyValuesRepository(
                        AzureStorage.Tables.AzureTableStorage<KeyValueEntity>.Create(conString, "KeyValues", c.Resolve<ILogFactory>()),
                        c.Resolve<IKeyValueHistoryRepository>(), c.Resolve<IMapper>()))
                .As<IKeyValuesRepository>()
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
            
            builder.Register(c =>
                    new NetworksRepository(
                        AzureStorage.Tables.AzureTableStorage<NetworkEntity>.Create(conString, "Networks", c.Resolve<ILogFactory>())))
                .As<INetworksRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new RepositoriesRepository(
                        AzureStorage.Tables.AzureTableStorage<RepositoryEntity>.Create(conString, "Repositories", c.Resolve<ILogFactory>()), c.Resolve<IMapper>()))
                .As<IRepositoriesRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new RepositoriesUpdateHistoryRepository(
                        AzureStorage.Tables.AzureTableStorage<RepositoryUpdateHistoryEntity>.Create(conString, "RepositoryUpdateHistory", c.Resolve<ILogFactory>()), c.Resolve<IMapper>()))
                .As<IRepositoryUpdateHistoryEventsRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new KeyValuesRepository(
                        AzureStorage.Tables.AzureTableStorage<KeyValueEntity>.Create(secretsConnString, "SecretKeyValues", c.Resolve<ILogFactory>()),
                        c.Resolve<IKeyValueHistoryRepository>(), c.Resolve<IMapper>()))
                .As<ISecretKeyValuesRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new RolesRepository(
                        AzureStorage.Tables.AzureTableStorage<RoleEntity>.Create(userConnString, "Role", c.Resolve<ILogFactory>()), c.Resolve<IMapper>()))
                .As<IRoleRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new ServiceTokenRepository(
                        AzureStorage.Tables.AzureTableStorage<ServiceTokenEntity>.Create(conString, "ServiceToken", c.Resolve<ILogFactory>()), c.Resolve<IMapper>()))
                .As<IServiceTokenRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new ServiceTokenHistoryRepository(
                        AzureStorage.Tables.AzureTableStorage<ServiceTokenHistoryEntity>.Create(conString, "ServiceTokenHistory", c.Resolve<ILogFactory>())))
                .As<IServiceTokenHistoryRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new TokensRepository(
                        AzureStorage.Tables.AzureTableStorage<TokenEntity>.Create(conString, "Tokens", c.Resolve<ILogFactory>()), c.Resolve<IMapper>()))
                .As<ITokensRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new UserActionHistoryRepository(
                        AzureStorage.Tables.AzureTableStorage<UserActionHistoryEntity>.Create(userConnString, "UserActionHistory", c.Resolve<ILogFactory>()),
                        new AzureBlobStorage(userConnString.CurrentValue), "useractionhistoryparam"))
                .As<IUserActionHistoryRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new UserSignInHistoryRepository(
                        AzureStorage.Tables.AzureTableStorage<UserSignInHistoryEntity>.Create(userConnString, "UserSignInHistory", c.Resolve<ILogFactory>())))
                .As<IUserSignInHistoryRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new UsersRepository(
                        AzureStorage.Tables.AzureTableStorage<UserEntity>.Create(userConnString, "User", c.Resolve<ILogFactory>()), c.Resolve<IMapper>()))
                .As<IUserRepository>()
                .SingleInstance();
            
            builder.Register(c =>
                    new UsersRepository(
                        AzureStorage.Tables.AzureTableStorage<UserEntity>.Create(userConnString, "User", c.Resolve<ILogFactory>()), c.Resolve<IMapper>()))
                .As<IUserRepository>()
                .SingleInstance();
            
            base.Load(builder);
        }
    }
}