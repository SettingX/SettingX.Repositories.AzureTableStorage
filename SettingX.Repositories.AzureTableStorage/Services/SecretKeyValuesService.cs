using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class SecretKeyValuesService : ISecretKeyValuesService
    {
        private readonly ISecretKeyValuesRepository _secretKeyValuesRepository;

        public SecretKeyValuesService(ISecretKeyValuesRepository secretKeyValuesRepository)
        {
            _secretKeyValuesRepository = secretKeyValuesRepository;
        }
        
        public Task<Dictionary<string, KeyValue>> GetAsync()
        {
            return _secretKeyValuesRepository.GetAsync();
        }

        public Task<KeyValue> GetTopRecordAsync()
        {
            return _secretKeyValuesRepository.GetTopRecordAsync();
        }

        public Task<List<KeyValue>> GetAsync(string id, string type)
        {
            return _secretKeyValuesRepository.GetAsync(id, type);
        }

        public Task<List<KeyValue>> GetKeyValuesAsync()
        {
            return _secretKeyValuesRepository.GetKeyValuesAsync();
        }

        public Task<List<KeyValue>> GetKeyValuesAsync(string keyRepoName, string filter, string search, string repositoryId = null)
        {
            return _secretKeyValuesRepository.GetKeyValuesAsync(keyRepoName, filter, search, repositoryId);
        }

        public Task<KeyValue> GetKeyValueAsync(string key)
        {
            return _secretKeyValuesRepository.GetKeyValueAsync(key);
        }

        public Task<Dictionary<string, KeyValue>> GetKeyValuesAsync(List<string> keys)
        {
            return _secretKeyValuesRepository.GetKeyValuesAsync(keys);
        }

        public Task<bool> UpdateKeyValueAsync(List<KeyValue> keyValueList)
        {
            return _secretKeyValuesRepository.UpdateKeyValueAsync(keyValueList);
        }

        public Task<bool> ReplaceKeyValueAsync(List<KeyValue> keyValueList)
        {
            return _secretKeyValuesRepository.ReplaceKeyValueAsync(keyValueList);
        }

        public Task RemoveNetworkOverridesAsync(string networkId)
        {
            return _secretKeyValuesRepository.RemoveNetworkOverridesAsync(networkId);
        }

        public Task DeleteKeyValueWithHistoryAsync(string keyValueId, string description, string userName, string userIpAddress)
        {
            return _secretKeyValuesRepository.DeleteKeyValueWithHistoryAsync(keyValueId, description, userName, userIpAddress);
        }
    }
}