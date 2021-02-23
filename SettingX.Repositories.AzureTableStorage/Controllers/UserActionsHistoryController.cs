using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/user_action_history")]
    public class UserActionsHistoryController : ControllerBase, IUserActionsHistoryApi
    {
        private readonly IUserActionsHistoryService _userActionsHistoryService;

        public UserActionsHistoryController(IUserActionsHistoryService userActionsHistoryService)
        {
            _userActionsHistoryService = userActionsHistoryService;
        }

        [HttpPost]
        public Task SaveUserActionHistoryAsync([FromBody] UserActionHistory userActionHistory)
        {
            return _userActionsHistoryService.SaveUserActionHistoryAsync(userActionHistory);
        }
    }
}