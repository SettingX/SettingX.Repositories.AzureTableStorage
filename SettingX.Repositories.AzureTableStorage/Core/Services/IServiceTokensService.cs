using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IServiceTokensService
    {
        Task<List<ServiceToken>> GetAllAsync();
        Task<ServiceToken> GetTopRecordAsync();
        Task<ServiceToken> GetAsync(string tokenKey);
        Task<bool> SaveOrUpdateAsync(ServiceToken token);
        Task<bool> RemoveAsync(string tokenId);
    }
}