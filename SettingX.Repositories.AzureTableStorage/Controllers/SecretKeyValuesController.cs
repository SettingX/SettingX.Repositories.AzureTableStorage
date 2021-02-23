using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/secret_key_value")]
    public class SecretKeyValuesController : ControllerBase, ISecretKeyValuesApi
    {
        private readonly ISecretKeyValuesService _secretKeyValuesService;

        public SecretKeyValuesController(ISecretKeyValuesService secretKeyValuesService)
        {
            _secretKeyValuesService = secretKeyValuesService;
        }

        [HttpGet("dict")]
        public Task<Dictionary<string, KeyValue>> GetAsync()
        {
            return _secretKeyValuesService.GetAsync();
        }

        [HttpGet("top")]
        public Task<KeyValue> GetTopRecordAsync()
        {
            return _secretKeyValuesService.GetTopRecordAsync();
        }

        [HttpGet("list")]
        public Task<List<KeyValue>> GetAsync([FromQuery] string id, [FromQuery] string type)
        {
            return _secretKeyValuesService.GetAsync(id, type);
        }

        [HttpGet("list/all")]
        public Task<List<KeyValue>> GetKeyValuesAsync()
        {
            return _secretKeyValuesService.GetKeyValuesAsync();
        }
        
        [HttpGet("ext")]
        public Task<List<KeyValue>> GetKeyValuesAsync([FromQuery] string keyRepoName, [FromQuery] string filter, [FromQuery] string search, [FromQuery] string repositoryId = null)
        {
            return _secretKeyValuesService.GetKeyValuesAsync(keyRepoName, filter, search, repositoryId);
        }

        [HttpGet("{key}")]
        public Task<KeyValue> GetKeyValueAsync([FromRoute] string key)
        {
            return _secretKeyValuesService.GetKeyValueAsync(key);
        }

        [HttpGet]
        public Task<Dictionary<string, KeyValue>> GetKeyValuesAsync([FromQuery] List<string> keys)
        {
            return _secretKeyValuesService.GetKeyValuesAsync(keys);
        }

        [HttpPut("update")]
        public Task<bool> UpdateKeyValueAsync([FromQuery] List<KeyValue> keyValueList)
        {
            return _secretKeyValuesService.UpdateKeyValueAsync(keyValueList);
        }

        [HttpPut("replace")]
        public Task<bool> ReplaceKeyValueAsync([FromQuery] List<KeyValue> keyValueList)
        {
            return _secretKeyValuesService.ReplaceKeyValueAsync(keyValueList);
        }

        [HttpDelete("network_overrides/{networkId}")]
        public Task RemoveNetworkOverridesAsync([FromRoute] string networkId)
        {
            return _secretKeyValuesService.RemoveNetworkOverridesAsync(networkId);
        }

        [HttpDelete("{keyValueId}")]
        public Task DeleteKeyValueWithHistoryAsync([FromRoute] string keyValueId, [FromQuery] string description, [FromQuery] string userName, [FromQuery] string userIpAddress)
        {
            return _secretKeyValuesService.DeleteKeyValueWithHistoryAsync(keyValueId, description, userName, userIpAddress);
        }
    }
}