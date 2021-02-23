using System;
using System.Threading.Tasks;
using AzureStorage;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.User
{
    public class UserSignInHistoryRepository : IUserSignInHistoryRepository
    {
        private readonly INoSQLTableStorage<UserSignInHistoryEntity> _tableStorage;

        public UserSignInHistoryRepository(INoSQLTableStorage<UserSignInHistoryEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task SaveUserLoginHistoryAsync(SettingX.Core.Models.User user, string userIpAddress)
        {
            var uh = new UserSignInHistoryEntity
            {
                PartitionKey = UserSignInHistoryEntity.GeneratePartitionKey(),
                UserEmail = user.Email,
                SignInDate = DateTime.UtcNow,
                IpAddress = userIpAddress
            };

            uh.RowKey = uh.GetRawKey();

            await _tableStorage.InsertOrMergeAsync(uh);
        }
    }
}