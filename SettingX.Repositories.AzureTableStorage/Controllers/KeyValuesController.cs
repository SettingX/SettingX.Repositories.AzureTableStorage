using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/key_value")]
    public class KeyValuesController : ControllerBase, IKeyValuesApi
    {
        private readonly IKeyValuesService _keyValuesService;

        public KeyValuesController(IKeyValuesService keyValuesService)
        {
            _keyValuesService = keyValuesService;
        }

        [HttpGet("dict")]
        public Task<Dictionary<string, KeyValue>> GetAsync()
        {
            return _keyValuesService.GetAsync();
        }

        [HttpGet("top")]
        public Task<KeyValue> GetTopRecordAsync()
        {
            return _keyValuesService.GetTopRecordAsync();
        }

        [HttpGet("list")]
        public Task<List<KeyValue>> GetAsync([FromQuery] string id, [FromQuery] string type)
        {
            return _keyValuesService.GetAsync(id, type);
        }

        [HttpGet("list/all")]
        public Task<List<KeyValue>> GetKeyValuesAsync()
        {
            return _keyValuesService.GetKeyValuesAsync();
        }
        
        [HttpGet("ext")]
        public Task<List<KeyValue>> GetKeyValuesAsync([FromQuery] string keyRepoName, [FromQuery] string filter, [FromQuery] string search, [FromQuery] string repositoryId = null)
        {
            return _keyValuesService.GetKeyValuesAsync(keyRepoName, filter, search, repositoryId);
        }

        [HttpGet("{key}")]
        public Task<KeyValue> GetKeyValueAsync([FromRoute] string key)
        {
            return _keyValuesService.GetKeyValueAsync(key);
        }

        [HttpGet]
        public Task<Dictionary<string, KeyValue>> GetKeyValuesAsync([FromQuery] List<string> keys)
        {
            return _keyValuesService.GetKeyValuesAsync(keys);
        }

        [HttpPut("update")]
        public Task<bool> UpdateKeyValueAsync([FromQuery] List<KeyValue> keyValueList)
        {
            return _keyValuesService.UpdateKeyValueAsync(keyValueList);
        }

        [HttpPut("replace")]
        public Task<bool> ReplaceKeyValueAsync([FromQuery] List<KeyValue> keyValueList)
        {
            return _keyValuesService.ReplaceKeyValueAsync(keyValueList);
        }

        [HttpDelete("network_overrides/{networkId}")]
        public Task RemoveNetworkOverridesAsync([FromRoute] string networkId)
        {
            return _keyValuesService.RemoveNetworkOverridesAsync(networkId);
        }

        [HttpDelete("{keyValueId}")]
        public Task DeleteKeyValueWithHistoryAsync([FromRoute] string keyValueId, [FromQuery] string description, [FromQuery] string userName, [FromQuery] string userIpAddress)
        {
            return _keyValuesService.DeleteKeyValueWithHistoryAsync(keyValueId, description, userName, userIpAddress);
        }
    }
}