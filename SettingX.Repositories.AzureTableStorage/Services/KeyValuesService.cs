using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class KeyValuesService : IKeyValuesService
    {
        private readonly IKeyValuesRepository _keyValuesRepository;

        public KeyValuesService(IKeyValuesRepository keyValuesRepository)
        {
            _keyValuesRepository = keyValuesRepository;
        }
        
        public Task<Dictionary<string, KeyValue>> GetAsync()
        {
            return _keyValuesRepository.GetAsync();
        }

        public Task<KeyValue> GetTopRecordAsync()
        {
            return _keyValuesRepository.GetTopRecordAsync();
        }

        public Task<List<KeyValue>> GetAsync(string id, string type)
        {
            return _keyValuesRepository.GetAsync(id, type);
        }

        public Task<List<KeyValue>> GetKeyValuesAsync()
        {
            return _keyValuesRepository.GetKeyValuesAsync();
        }

        public Task<List<KeyValue>> GetKeyValuesAsync(string keyRepoName, string filter, string search, string repositoryId = null)
        {
            return _keyValuesRepository.GetKeyValuesAsync(keyRepoName, filter, search, repositoryId);
        }

        public Task<KeyValue> GetKeyValueAsync(string key)
        {
            return _keyValuesRepository.GetKeyValueAsync(key);
        }

        public Task<Dictionary<string, KeyValue>> GetKeyValuesAsync(List<string> keys)
        {
            return _keyValuesRepository.GetKeyValuesAsync(keys);
        }

        public Task<bool> UpdateKeyValueAsync(List<KeyValue> keyValueList)
        {
            return _keyValuesRepository.UpdateKeyValueAsync(keyValueList);
        }

        public Task<bool> ReplaceKeyValueAsync(List<KeyValue> keyValueList)
        {
            return _keyValuesRepository.ReplaceKeyValueAsync(keyValueList);
        }

        public Task RemoveNetworkOverridesAsync(string networkId)
        {
            return _keyValuesRepository.RemoveNetworkOverridesAsync(networkId);
        }

        public Task DeleteKeyValueWithHistoryAsync(string keyValueId, string description, string userName, string userIpAddress)
        {
            return _keyValuesRepository.DeleteKeyValueWithHistoryAsync(keyValueId, description, userName, userIpAddress);
        }
    }
}