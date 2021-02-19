using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/lock")]
    public class EditLocksController : ControllerBase, ILocksApi
    {
        private readonly IEditLockService _editLockService;

        public EditLocksController(IEditLockService editLockService)
        {
            _editLockService = editLockService;
        }

        [HttpGet]
        public Task<EditLock> GetJsonPageLockAsync()
        {
            return _editLockService.GetJsonPageLockAsync();
        }

        [HttpPost]
        public Task SetJsonPageLockAsync([FromQuery] string userEmail, [FromQuery] string userName,
            [FromQuery] string ipAddress)
        {
            return _editLockService.SetJsonPageLockAsync(userEmail, userName, ipAddress);
        }

        [HttpPut]
        public Task ResetJsonPageLockAsync()
        {
            return _editLockService.ResetJsonPageLockAsync();
        }
    }
}