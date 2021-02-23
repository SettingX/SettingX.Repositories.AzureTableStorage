using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/repositories_update_history")]
    public class RepositoriesUpdateHistoryController : ControllerBase, IRepositoriesUpdateHistoryApi
    {
        private readonly IRepositoriesUpdateHistoryService _repositoriesUpdateHistoryService;

        public RepositoriesUpdateHistoryController(IRepositoriesUpdateHistoryService repositoriesUpdateHistoryService)
        {
            _repositoriesUpdateHistoryService = repositoriesUpdateHistoryService;
        }

        [HttpGet("{repositoryUpdateHistoryId}")]
        public Task<RepositoryUpdateHistoricEvent> GetAsync(string repositoryUpdateHistoryId)
        {
            return _repositoriesUpdateHistoryService.GetAsync(repositoryUpdateHistoryId);
        }

        [HttpPost]
        public Task SaveRepositoryUpdateHistory([FromBody] RepositoryUpdateHistoricEvent entity)
        {
            return _repositoriesUpdateHistoryService.SaveRepositoryUpdateHistoryAsync(entity);
        }

        [HttpGet("list/{initialCommit}")]
        public Task<List<RepositoryUpdateHistoricEvent>> GetByInitialCommitAsync([FromRoute] string initialCommit)
        {
            return _repositoriesUpdateHistoryService.GetByInitialCommitAsync(initialCommit);
        }

        [HttpDelete]
        public Task RemoveRepositoryUpdateHistoryAsync(List<string> repositoryUpdateHistoryIds)
        {
            return _repositoriesUpdateHistoryService.RemoveRepositoryUpdateHistoryAsync(repositoryUpdateHistoryIds);
        }
    }
}