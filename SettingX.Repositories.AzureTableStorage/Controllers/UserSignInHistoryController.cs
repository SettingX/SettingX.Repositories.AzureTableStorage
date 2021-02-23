using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/user_signin_history")]
    public class UserSignInHistoryController : ControllerBase, IUserSignInHistoryApi
    {
        private readonly IUserSignInHistoryService _userSignInHistoryService;

        public UserSignInHistoryController(IUserSignInHistoryService userSignInHistoryService)
        {
            _userSignInHistoryService = userSignInHistoryService;
        }

        [HttpPost]
        public Task SaveUserLoginAsync([FromBody] User user, [FromQuery] string userIpAddress)
        {
            return _userSignInHistoryService.SaveUserLoginAsync(user, userIpAddress);
        }
    }
}