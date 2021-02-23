using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("roles")]
    public class RolesController : ControllerBase, IRolesApi
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet("{roleId}")]
        public Task<Role> GetAsync([FromRoute] string roleId)
        {
            return _rolesService.GetAsync(roleId);
        }

        [HttpGet("byName/{roleName}")]
        public Task<Role> GetByNameAsync([FromRoute] string roleName)
        {
            return _rolesService.GetByNameAsync(roleName);
        }

        [HttpGet]
        public Task<List<Role>> GetAllAsync()
        {
            return _rolesService.GetAllAsync();
        }

        [HttpGet("byRoles")]
        public Task<List<Role>> FindAsync([FromQuery] List<string> roleIds)
        {
            return _rolesService.FindAsync(roleIds);
        }

        [HttpPost]
        public Task SaveAsync([FromBody] Role entity)
        {
            return _rolesService.SaveAsync(entity);
        }

        [HttpDelete("{roleId}")]
        public Task RemoveAsync([FromRoute] string roleId)
        {
            return _rolesService.RemoveAsync(roleId);
        }
    }
}