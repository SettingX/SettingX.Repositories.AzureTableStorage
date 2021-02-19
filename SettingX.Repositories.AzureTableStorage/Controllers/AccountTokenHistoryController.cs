using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/account_token_history")]
    public class AccountTokenHistoryController : ControllerBase, IAccountTokenHistoryApi
    {
        private readonly IAccountTokenHistoryService _accountTokenHistoryService;

        public AccountTokenHistoryController(IAccountTokenHistoryService accountTokenHistoryService)
        {
            _accountTokenHistoryService = accountTokenHistoryService;
        }

        [HttpPost]
        public Task SaveTokenHistoryAsync([FromBody] Token token, [FromQuery] string userName, [FromQuery] string userIpAddress)
        {
            return _accountTokenHistoryService.SaveTokenHistoryAsync(token, userName, userIpAddress);
        }
    }
}