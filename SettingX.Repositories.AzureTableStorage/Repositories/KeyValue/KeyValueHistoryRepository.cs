using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureStorage;
using Newtonsoft.Json;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using IBlobStorage = SettingX.Core.Blob.IBlobStorage;

namespace SettingX.Repositories.AzureTableStorage.Repositories.KeyValue
{
    public class KeyValueHistoryRepository : IKeyValueHistoryRepository
    {
        private readonly INoSQLTableStorage<KeyValueHistoricEntity> _tableStorage;
        private readonly IBlobStorage _blobStorage;
        private readonly string _container;

        public KeyValueHistoryRepository(
            INoSQLTableStorage<KeyValueHistoricEntity> tableStorage,
            IBlobStorage blobStorage,
            string container)
        {
            _tableStorage = tableStorage;
            _blobStorage = blobStorage;
            _container = container;
        }

        public async Task SaveKeyValueOverrideHistoryAsync(
            string keyValueId,
            string newOverride,
            string keyValues,
            string userName,
            string userIpAddress)
        {
            var datetime = DateTime.UtcNow.StorageString();
            var th = new KeyValueHistoricEntity
            {
                PartitionKey = keyValueId,
                RowKey = datetime,
                DateTime = datetime,
                KeyValueId = keyValueId,
                NewOverride = newOverride,
                UserName = userName,
                UserIpAddress = userIpAddress
            };

            th.KeyValuesSnapshot = $"{th.UserName}_{th.RowKey}_{th.UserIpAddress}";

            await _blobStorage.SaveBlobAsync(_container, th.KeyValuesSnapshot, Encoding.UTF8.GetBytes(keyValues));

            await _tableStorage.InsertOrMergeAsync(th);
        }

        public async Task SaveKeyValueHistoryAsync(
            string keyValueId,
            string newValue,
            string keyValues,
            string userName,
            string userIpAddress)
        {
            var datetime = DateTime.UtcNow.StorageString();
            var th = new KeyValueHistoricEntity
            {
                PartitionKey = keyValueId,
                RowKey = datetime,
                DateTime = datetime,
                KeyValueId = keyValueId,
                NewValue = newValue,
                UserName = userName,
                UserIpAddress = userIpAddress
            };

            th.KeyValuesSnapshot = $"{th.UserName}_{th.RowKey}_{th.UserIpAddress}";

            await _blobStorage.SaveBlobAsync(_container, th.KeyValuesSnapshot, Encoding.UTF8.GetBytes(keyValues));

            await _tableStorage.InsertOrMergeAsync(th);
        }

        public Task SaveKeyValuesHistoryAsync(
            List<SettingX.Core.Models.KeyValue> keyValues,
            string userName,
            string userIpAddress)
        {
            var timestamp = DateTime.UtcNow.StorageString();
            var snapshotName = $"{userName}_{timestamp}_{userIpAddress}";
            string valuesSnapshot = JsonConvert.SerializeObject(keyValues);
            var snapshotBytes = Encoding.UTF8.GetBytes(valuesSnapshot);
            //Don't await it, cause it's too heavy and doesn't affect subsequent operations.
            Task.Run(async () => await _blobStorage.SaveBlobAsync(_container, snapshotName, snapshotBytes));

            return Task.WhenAll(keyValues.Select(k =>
                _tableStorage.InsertOrMergeAsync(new KeyValueHistoricEntity
                {
                    PartitionKey = k.KeyValueId,
                    RowKey = timestamp,
                    DateTime = timestamp,
                    KeyValueId = k.KeyValueId,
                    NewValue = k.Value,
                    UserName = userName,
                    UserIpAddress = userIpAddress,
                    KeyValuesSnapshot = snapshotName,
                })));
        }

        public async Task DeleteKeyValueHistoryAsync(string keyValueId, string description, string userName, string userIpAddress)
        {
            var datetime = DateTime.UtcNow.StorageString();
            var th = new KeyValueHistoricEntity
            {
                PartitionKey = keyValueId,
                RowKey = datetime,
                DateTime = datetime,
                KeyValueId = keyValueId,
                UserName = userName,
                UserIpAddress = userIpAddress
            };

            th.KeyValuesSnapshot = $"{th.UserName}_{th.RowKey}_{th.UserIpAddress}";

            await _blobStorage.SaveBlobAsync(_container, th.KeyValuesSnapshot, Encoding.UTF8.GetBytes(description));

            await _tableStorage.InsertOrMergeAsync(th);
        }

        public async Task<List<KeyValueHistoricEvent>> GetHistoryByKeyValueAsync(string keyValueId)
        {
            if (string.IsNullOrEmpty(keyValueId))
            {
                return new List<KeyValueHistoricEvent>();
            }

            var hist = await _tableStorage.GetDataAsync(keyValueId, v => v.KeyValueId == keyValueId);

            return hist
                .OrderByDescending(h => h.Timestamp)
                .Cast<KeyValueHistoricEvent>()
                .ToList();
        }
    }
}