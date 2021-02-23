using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class RepositoriesService
    {
        private readonly IRepositoriesRepository _repositoriesRepository;

        public RepositoriesService(IRepositoriesRepository repositoriesRepository)
        {
            _repositoriesRepository = repositoriesRepository;
        }
        
        public Task<Repository> GetAsync(string repositoryId)
        {
            return _repositoriesRepository.GetAsync(repositoryId);
        }

        public Task<List<Repository>> GetAllAsync()
        {
            return _repositoriesRepository.GetAllAsync();
        }

        public Task RemoveRepositoryAsync(string repositoryId)
        {
            return _repositoriesRepository.RemoveRepositoryAsync(repositoryId);
        }

        public Task SaveRepositoryAsync(Repository repository)
        {
            return _repositoriesRepository.SaveRepositoryAsync(repository);
        }
    }
}