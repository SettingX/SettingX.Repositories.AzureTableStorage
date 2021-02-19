using System;
using System.Threading.Tasks;
using AzureStorage;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Token
{
    public class AccountTokenHistoryRepository : IAccountTokenHistoryRepository
    {
        private readonly INoSQLTableStorage<AccountTokenHistoryEntity> _tableStorage;

        public AccountTokenHistoryRepository(INoSQLTableStorage<AccountTokenHistoryEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task SaveTokenHistoryAsync(SettingX.Core.Models.Token token, string userName, string userIpAddress)
        {
            var th = new AccountTokenHistoryEntity
            {
                PartitionKey = token.TokenId,
                RowKey = DateTime.UtcNow.StorageString(),
                UserName = userName,
                AccessList = token.AccessList,
                IpList = token.IpList,
                TokenId = token.TokenId,
                UserIpAddress = userIpAddress
            };

            await _tableStorage.InsertOrMergeAsync(th);
        }
    }
}