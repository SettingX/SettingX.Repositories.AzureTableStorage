using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IRepositoryDataService
    {
        Task<string> GetDataAsync(string file = null);
        Task UpdateAsync(string json, string userName, string ipAddress, string file = null);
        Task<List<string>> GetExistingFileNamesAsync();
        Task<bool> ExistsAsync(string file = null);
        Task DeleteAsync(string file);
    }
}