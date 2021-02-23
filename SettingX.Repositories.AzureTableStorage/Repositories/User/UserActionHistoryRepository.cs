using System;
using System.Text;
using System.Threading.Tasks;
using AzureStorage;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using IBlobStorage = SettingX.Core.Blob.IBlobStorage;

namespace SettingX.Repositories.AzureTableStorage.Repositories.User
{
    public class UserActionHistoryRepository : IUserActionHistoryRepository
    {
        private readonly INoSQLTableStorage<UserActionHistoryEntity> _tableStorage;
        private readonly IBlobStorage _blobStorage;
        private readonly string _container;

        public UserActionHistoryRepository(INoSQLTableStorage<UserActionHistoryEntity> tableStorage, IBlobStorage blobStorage, string container)
        {
            _tableStorage = tableStorage;
            _blobStorage = blobStorage;
            _container = container;
        }

        public async Task SaveUserActionHistoryAsync(UserActionHistory userActionHistory)
        {
            var entity = UserActionHistoryEntity.Create(userActionHistory);
            if (!string.IsNullOrEmpty(entity.Params))
            {
                var parms = entity.Params;
                entity.Params = Guid.NewGuid().ToString();

                if (!string.IsNullOrEmpty(parms))
                {
                    var data = Encoding.UTF8.GetBytes(parms);
                    await _blobStorage.SaveBlobAsync(_container, entity.Params, data);
                }
            }
           
            await _tableStorage.InsertOrMergeAsync(entity);
        }
    }
}