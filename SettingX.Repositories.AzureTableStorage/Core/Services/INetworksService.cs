using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface INetworksService
    {
        Task<List<Network>> GetAllAsync();
        Task<Network> GetByIpAsync(string ip);
        Task<bool> NetworkExistsAsync(string id);
        Task AddAsync(Network network);
        Task UpdateAsync(Network network);
        Task DeleteAsync(string id);
    }
}