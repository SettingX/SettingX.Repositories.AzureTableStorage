using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/service_tokens")]
    public class ServiceTokensController : IServiceTokensApi
    {
        private readonly IServiceTokensService _serviceTokensService;

        public ServiceTokensController(IServiceTokensService serviceTokensService)
        {
            _serviceTokensService = serviceTokensService;
        }

        [HttpGet]
        public Task<List<ServiceToken>> GetAllAsync()
        {
            return _serviceTokensService.GetAllAsync();
        }

        [HttpGet("top")]
        public Task<ServiceToken> GetTopRecordAsync()
        {
            return _serviceTokensService.GetTopRecordAsync();
        }
        
        [HttpGet("{tokenKey}")]
        public Task<ServiceToken> GetAsync([FromRoute] string tokenKey)
        {
            return _serviceTokensService.GetAsync(tokenKey);
        }

        [HttpPost]
        public Task<bool> SaveOrUpdateAsync([FromBody] ServiceToken token)
        {
            return _serviceTokensService.SaveOrUpdateAsync(token);
        }

        [HttpDelete("{tokenId}")]
        public Task<bool> RemoveAsync([FromRoute] string tokenId)
        {
            return _serviceTokensService.RemoveAsync(tokenId);
        }
    }
}