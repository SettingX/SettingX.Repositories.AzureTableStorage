using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class RepositoryDataService : IRepositoryDataService
    {
        private readonly IRepositoryDataRepository _repositoryDataRepository;

        public RepositoryDataService(IRepositoryDataRepository repositoryDataRepository)
        {
            _repositoryDataRepository = repositoryDataRepository;
        }

        public Task<string> GetDataAsync(string file = null)
        {
            return _repositoryDataRepository.GetDataAsync(file);
        }

        public Task UpdateAsync(string json, string userName, string ipAddress, string file = null)
        {
            return _repositoryDataRepository.UpdateBlobAsync(json, userName, ipAddress, file);
        }

        public Task<List<string>> GetExistingFileNamesAsync()
        {
            return _repositoryDataRepository.GetExistingFileNamesAsync();
        }

        public Task<bool> ExistsAsync(string file = null)
        {
            return _repositoryDataRepository.ExistsAsync(file);
        }

        public Task DeleteAsync(string file)
        {
            return _repositoryDataRepository.DelBlobAsync(file);
        }
    }
}