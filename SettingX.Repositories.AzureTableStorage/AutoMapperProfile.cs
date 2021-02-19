using AutoMapper;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Repositories.KeyValue;
using SettingX.Repositories.AzureTableStorage.Repositories.Lock;
using SettingX.Repositories.AzureTableStorage.Repositories.Repository;

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
        }
    }
}