using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase, IUsersApi
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("byEmail")]
        public Task<User> GetUserByUserEmailAsync([FromQuery] string userEmail, [FromQuery] string passwordHash)
        {
            return _usersService.GetUserByUserEmailAsync(userEmail, passwordHash);
        }

        [HttpPost("admin")]
        public Task CreateInitialAdminAsync([FromQuery] string defaultUserEmail, [FromQuery] string defaultUserPasswordHash)
        {
            return _usersService.CreateInitialAdminAsync(defaultUserEmail, defaultUserPasswordHash);
        }
        
        [HttpPost]
        public Task CreateUserAsync([FromBody] User user)
        {
            return _usersService.CreateUserAsync(user);
        }
        
        [HttpPut]
        public Task<bool> UpdateUserAsync([FromBody] User user)
        {
            return _usersService.UpdateUserAsync(user);
        }
        
        [HttpGet]
        public Task<List<User>> GetUsersAsync()
        {
            return _usersService.GetUsersAsync();
        }
        
        [HttpGet("top")]
        public Task<User> GetTopUserRecordAsync()
        {
            return _usersService.GetTopUserRecordAsync();
        }
        
        [HttpDelete]
        public Task<bool> RemoveUserAsync([FromQuery] string userEmail)
        {
            return _usersService.RemoveUserAsync(userEmail);
        }
    }
}