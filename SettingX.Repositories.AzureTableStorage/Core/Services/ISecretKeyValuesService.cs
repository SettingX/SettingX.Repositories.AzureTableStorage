using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface ISecretKeyValuesService : IKeyValuesService
    {
        /*Task<Dictionary<string, KeyValue>> GetAsync();
        Task<KeyValue> GetTopRecordAsync();
        Task<List<KeyValue>> GetAsync(string id, string type);
        Task<List<KeyValue>> GetKeyValuesAsync();
        Task<List<KeyValue>> GetKeyValuesAsync(string keyRepoName, string filter, string search, string repositoryId = null);
        Task<KeyValue> GetKeyValueAsync(string key);
        Task<Dictionary<string, KeyValue>> GetKeyValuesAsync(List<string> keys);
        Task<bool> UpdateKeyValueAsync(List<KeyValue> keyValueList);
        Task<bool> ReplaceKeyValueAsync(List<KeyValue> keyValueList);
        Task RemoveNetworkOverridesAsync(string networkId);
        Task DeleteKeyValueWithHistoryAsync(
            string keyValueId,
            string description,
            string userName,
            string userIpAddress);
            */
    }
}