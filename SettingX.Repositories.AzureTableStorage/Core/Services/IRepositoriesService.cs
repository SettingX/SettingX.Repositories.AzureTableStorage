using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IRepositoriesService
    {
        Task<Repository> GetAsync(string repositoryId);
        Task<List<Repository>> GetAllAsync();
        Task RemoveRepositoryAsync(string repositoryId);
        Task SaveRepositoryAsync(Repository repository);
    }
}