using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/repositories")]
    public class RepositoriesController : ControllerBase, IRepositoriesApi
    {
        private readonly IRepositoriesService _repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            _repositoriesService = repositoriesService;
        }

        [HttpGet("{repositoryId}")]
        public Task<Repository> GetAsync([FromRoute] string repositoryId)
        {
            return _repositoriesService.GetAsync(repositoryId);
        }

        [HttpGet]
        public Task<List<Repository>> GetAllAsync()
        {
            return _repositoriesService.GetAllAsync();
        }

        [HttpDelete("{repositoryId}")]
        public Task RemoveRepositoryAsync([FromRoute] string repositoryId)
        {
            return _repositoriesService.RemoveRepositoryAsync(repositoryId);
        }

        [HttpPost]
        public Task SaveRepositoryAsync([FromBody] Repository repository)
        {
            return _repositoriesService.SaveRepositoryAsync(repository);
        }
    }
}