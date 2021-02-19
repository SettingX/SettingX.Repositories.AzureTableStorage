using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class KeyValuesHistoryService : IKeyValuesHistoryService
    {
        private readonly IKeyValueHistoryRepository _keyValueHistoryRepository;

        public KeyValuesHistoryService(IKeyValueHistoryRepository keyValueHistoryRepository)
        {
            _keyValueHistoryRepository = keyValueHistoryRepository;
        }

        public Task SaveKeyValueHistoryAsync(string keyValueId, string newValue, string keyValues, string userName,
            string userIpAddress)
        {
            return _keyValueHistoryRepository.SaveKeyValueHistoryAsync(keyValueId, newValue, keyValues, userName,
                userIpAddress);
        }

        public Task SaveKeyValuesHistoryAsync(IEnumerable<KeyValue> keyValues, string userName, string userIpAddress)
        {
            return _keyValueHistoryRepository.SaveKeyValuesHistoryAsync(keyValues, userName, userIpAddress);
        }

        public Task SaveKeyValueOverrideHistoryAsync(string keyValueId, string newOverride, string keyValues, string userName,
            string userIpAddress)
        {
            return _keyValueHistoryRepository.SaveKeyValueOverrideHistoryAsync(keyValueId, newOverride, keyValues, userName, userIpAddress);
        }

        public Task DeleteKeyValueHistoryAsync(string keyValueId, string description, string userName, string userIpAddress)
        {
            return _keyValueHistoryRepository.DeleteKeyValueHistoryAsync(keyValueId, description, userName,
                userIpAddress);
        }

        public Task<List<KeyValueHistoricEvent>> GetHistoryByKeyValueAsync(string keyValueId)
        {
            return _keyValueHistoryRepository.GetHistoryByKeyValueAsync(keyValueId);
        }
    }
}