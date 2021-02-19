using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/key_values_history")]
    public class KeyValuesHistoryController : ControllerBase, IKeyValuesHistoryApi
    {
        private readonly IKeyValuesHistoryService _keyValuesHistoryService;

        public KeyValuesHistoryController(IKeyValuesHistoryService keyValuesHistoryService)
        {
            _keyValuesHistoryService = keyValuesHistoryService;
        }

        [HttpPost("{keyValueId}")]
        public Task SaveKeyValueHistoryAsync(
            [FromRoute] string keyValueId,
            [FromQuery] string newValue,
            [FromQuery] string keyValues, 
            [FromQuery] string userName,
            [FromQuery] string userIpAddress)
        {
            return _keyValuesHistoryService.SaveKeyValueHistoryAsync(keyValueId, newValue, keyValues, userName,
                userIpAddress);
        }

        [HttpPost("multiple")]
        public Task SaveKeyValuesHistoryAsync(
            [FromBody] List<KeyValue> keyValues,
            [FromQuery] string userName,
            [FromQuery] string userIpAddress)
        {
            return _keyValuesHistoryService.SaveKeyValuesHistoryAsync(keyValues, userName, userIpAddress);
        }

        [HttpPost("override/{keyValueId}")]
        public Task SaveKeyValueOverrideHistoryAsync(
            [FromRoute] string keyValueId,
            [FromQuery] string newOverride,
            [FromQuery] string keyValues,
            [FromQuery] string userName,
            [FromQuery] string userIpAddress)
        {
            return _keyValuesHistoryService.SaveKeyValueOverrideHistoryAsync(keyValueId, newOverride, keyValues, userName, userIpAddress);
        }

        [HttpDelete("{keyValueId}")]
        public Task DeleteKeyValueHistoryAsync(
            [FromRoute] string keyValueId,
            [FromQuery] string description,
            [FromQuery] string userName,
            [FromQuery] string userIpAddress)
        {
            return _keyValuesHistoryService.DeleteKeyValueHistoryAsync(keyValueId, description, userName,
                userIpAddress);
        }

        [HttpGet("{keyValueId}")]
        public Task<List<KeyValueHistoricEvent>> GetHistoryByKeyValueAsync([FromRoute] string keyValueId)
        {
            return _keyValuesHistoryService.GetHistoryByKeyValueAsync(keyValueId);
        }
    }
}