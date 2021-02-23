using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/service_token_history")]
    public class ServiceTokenHistoryController : ControllerBase, IServiceTokenHistoryApi
    {
        private readonly IServiceTokensHistoryService _serviceTokensHistoryService;

        public ServiceTokenHistoryController(IServiceTokensHistoryService serviceTokensHistoryService)
        {
            _serviceTokensHistoryService = serviceTokensHistoryService;
        }

        [HttpPost]
        public Task SaveTokenHistoryAsync([FromBody] ServiceToken token, [FromQuery] string userName, [FromQuery] string userIpAddress)
        {
            return _serviceTokensHistoryService.SaveTokenHistoryAsync(token, userName, userIpAddress);
        }
    }
}