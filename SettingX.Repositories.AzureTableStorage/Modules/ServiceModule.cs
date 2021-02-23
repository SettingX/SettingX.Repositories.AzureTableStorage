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
            
            builder.RegisterType<AccountTokenHistoryService>()
                .As<IAccountTokenHistoryService>()
                .SingleInstance();
            
            builder.RegisterType<ConnectionUrlHistoryService>()
                .As<IConnectionUrlHistoryService>()
                .SingleInstance();
            
            builder.RegisterType<KeyValuesService>()
                .As<IKeyValuesService>()
                .SingleInstance();
            
            builder.RegisterType<KeyValuesHistoryService>()
                .As<IKeyValuesHistoryService>()
                .SingleInstance();
            
            builder.RegisterType<NetworksService>()
                .As<INetworksService>()
                .SingleInstance();
            
            builder.RegisterType<RepositoriesUpdateHistoryService>()
                .As<IRepositoriesUpdateHistoryService>()
                .SingleInstance();
            
            builder.RegisterType<SecretKeyValuesService>()
                .As<ISecretKeyValuesService>()
                .SingleInstance();
            
            builder.RegisterType<RolesService>()
                .As<IRolesService>()
                .SingleInstance();
            
            builder.RegisterType<TokensService>()
                .As<ITokensService>()
                .SingleInstance();
            
            builder.RegisterType<ServiceTokensService>()
                .As<IServiceTokensService>()
                .SingleInstance();
            
            builder.RegisterType<ServiceTokensHistoryService>()
                .As<IServiceTokensHistoryService>()
                .SingleInstance();
            
            builder.RegisterType<UserActionsHistoryService>()
                .As<IUserActionsHistoryService>()
                .SingleInstance();
            
            builder.RegisterType<UserSignInHistoryService>()
                .As<IUserSignInHistoryService>()
                .SingleInstance();
            
            builder.RegisterType<UsersService>()
                .As<IUsersService>()
                .SingleInstance();
        }
    }
}