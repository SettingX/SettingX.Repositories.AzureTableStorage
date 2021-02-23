using System;
using System.Threading.Tasks;
using AzureStorage;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.ServiceToken
{
    public class ServiceTokenHistoryRepository : IServiceTokenHistoryRepository
    {
        private readonly INoSQLTableStorage<ServiceTokenHistoryEntity> _tableStorage;

        public ServiceTokenHistoryRepository(INoSQLTableStorage<ServiceTokenHistoryEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }
        
        public async Task SaveTokenHistoryAsync(SettingX.Core.Models.ServiceToken token, string userName, string userIpAddress)
        {
            var th = new ServiceTokenHistoryEntity
            {
                PartitionKey = token.Token,
                RowKey = DateTime.UtcNow.StorageString(),
                UserName = userName,
                KeyOne = token.SecurityKeyOne,
                KeyTwo = token.SecurityKeyTwo,
                TokenId = token.Token,
                UserIpAddress = userIpAddress
            };

            await _tableStorage.InsertOrMergeAsync(th);
        }
    }
}