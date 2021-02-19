using System;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using SettingX.Core.Models;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Lock
{
    public class EditLockRepository : IEditLockRepository
    {
        private readonly INoSQLTableStorage<EditLockEntity> _tableStorage;
        private const string JsonLockKey = "jsonLock";
        private readonly IMapper _mapper;

        public EditLockRepository(INoSQLTableStorage<EditLockEntity> tableStorage, IMapper mapper)
        {
            _tableStorage = tableStorage;
            _mapper = mapper;
        }

        public async Task<EditLock> GetJsonPageLockAsync()
        {
            var pk = EditLockEntity.GeneratePartitionKey();
            return _mapper.Map<EditLock>(await _tableStorage.GetDataAsync(pk, JsonLockKey));
        }

        public async Task SetJsonPageLockAsync(string userEmail, string userName, string ipAddress)
        {
            await _tableStorage.InsertOrMergeAsync(new EditLockEntity
            {
                PartitionKey = EditLockEntity.GeneratePartitionKey(),
                RowKey = JsonLockKey,
                UserEmail = userEmail,
                DateTime = DateTime.UtcNow,
                UserName = userName,
                IpAddress = ipAddress,
            });
        }

        public async Task ResetJsonPageLockAsync()
        {
            await _tableStorage.InsertOrReplaceAsync(new EditLockEntity
            {
                PartitionKey = EditLockEntity.GeneratePartitionKey(),
                RowKey = JsonLockKey,
                DateTime = new DateTime(1701, 1, 1), //Storage Azure can't store less
            });
        }
    }
}