using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/networks")]
    public class NetworksController : ControllerBase, INetworksApi
    {
        private readonly INetworksService _networksService;

        public NetworksController(INetworksService networksService)
        {
            _networksService = networksService;
        }
        
        [HttpGet]
        public Task<List<Network>> GetAllAsync()
        {
            return _networksService.GetAllAsync();
        }

        [HttpGet("{ip}")]
        public Task<Network> GetByIpAsync([FromRoute] string ip)
        {
            return _networksService.GetByIpAsync(ip);
        }
        
        [HttpPost]
        public Task AddAsync([FromBody] Network network)
        {
            return _networksService.AddAsync(network);
        }

        [HttpPut]
        public Task UpdateAsync([FromBody] Network network)
        {
            return _networksService.UpdateAsync(network);
        }
        
        [HttpDelete("{id}")]
        public Task DeleteAsync(string id)
        {
            return _networksService.DeleteAsync(id);
        }
    }
}