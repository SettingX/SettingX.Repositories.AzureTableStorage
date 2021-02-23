using AutoMapper;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Repositories.KeyValue;
using SettingX.Repositories.AzureTableStorage.Repositories.Lock;
using SettingX.Repositories.AzureTableStorage.Repositories.Network;
using SettingX.Repositories.AzureTableStorage.Repositories.Repository;
using SettingX.Repositories.AzureTableStorage.Repositories.ServiceToken;
using SettingX.Repositories.AzureTableStorage.Repositories.User;

namespace SettingX.Repositories.AzureTableStorage
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ConnectionUrlHistoricEntity, ConnectionUrlHistoricEvent>().ReverseMap();
            CreateMap<KeyValue, KeyValueEntity>().ReverseMap();
            CreateMap<KeyValueHistoricEvent, KeyValueHistoricEntity>().ReverseMap();
            CreateMap<ConnectionUrlHistoricEntity, ConnectionUrlHistoricEvent>().ReverseMap();
            CreateMap<EditLock, EditLockEntity>().ReverseMap();
            CreateMap<Network, NetworkEntity>().ReverseMap();
            CreateMap<Repository, RepositoryEntity>().ReverseMap();
            CreateMap<Role, RoleEntity>().ReverseMap();
            CreateMap<RepositoryUpdateHistoricEvent, RepositoryUpdateHistoryEntity>().ReverseMap();
            CreateMap<ServiceToken, ServiceTokenEntity>().ReverseMap();
        }
    }
}