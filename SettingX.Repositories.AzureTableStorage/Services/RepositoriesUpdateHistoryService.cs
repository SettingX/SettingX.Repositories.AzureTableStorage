using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class RepositoriesUpdateHistoryService : IRepositoriesUpdateHistoryService
    {
        private readonly IRepositoryUpdateHistoryEventsRepository _repositoryUpdateHistoryEventsRepository;

        public RepositoriesUpdateHistoryService(IRepositoryUpdateHistoryEventsRepository repositoryUpdateHistoryEventsRepository)
        {
            _repositoryUpdateHistoryEventsRepository = repositoryUpdateHistoryEventsRepository;
        }

        public Task<RepositoryUpdateHistoricEvent> GetAsync(string repositoryUpdateHistoryId)
        {
            return _repositoryUpdateHistoryEventsRepository.GetAsync(repositoryUpdateHistoryId);
        }

        public Task SaveRepositoryUpdateHistoryAsync(RepositoryUpdateHistoricEvent entity)
        {
            return _repositoryUpdateHistoryEventsRepository.SaveRepositoryUpdateHistoryAsync(entity);
        }

        public Task<List<RepositoryUpdateHistoricEvent>> GetByInitialCommitAsync(string initialCommit)
        {
            return _repositoryUpdateHistoryEventsRepository.GetByInitialCommitAsync(initialCommit);
        }

        public Task RemoveRepositoryUpdateHistoryAsync(List<string> repositoryUpdateHistoryIds)
        {
            return _repositoryUpdateHistoryEventsRepository.RemoveRepositoryUpdateHistoryAsync(repositoryUpdateHistoryIds);
        }
    }
}