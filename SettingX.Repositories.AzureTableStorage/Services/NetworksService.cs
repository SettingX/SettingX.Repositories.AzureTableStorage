using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class NetworksService : INetworksService
    {
        private readonly INetworksRepository _networksRepository;

        public NetworksService(INetworksRepository networksRepository)
        {
            _networksRepository = networksRepository;
        }

        public Task<List<Network>> GetAllAsync()
        {
            return _networksRepository.GetAllAsync();
        }

        public Task<Network> GetByIpAsync(string ip)
        {
            return _networksRepository.GetByIpAsync(ip);
        }

        public Task<bool> NetworkExistsAsync(string id)
        {
            return _networksRepository.NetworkExistsAsync(id);
        }

        public Task AddAsync(Network network)
        {
            return _networksRepository.AddAsync(network);
        }

        public Task UpdateAsync(Network network)
        {
            return _networksRepository.UpdateAsync(network);
        }

        public Task DeleteAsync(string id)
        {
            return _networksRepository.DeleteAsync(id);
        }
    }
}