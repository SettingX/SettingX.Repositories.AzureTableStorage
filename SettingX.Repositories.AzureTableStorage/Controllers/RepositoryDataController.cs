using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/repository_data")]
    public class RepositoryDataController : ControllerBase, IRepositoryDataApi
    {
        private readonly IRepositoryDataService _repositoryDataService;

        public RepositoryDataController(IRepositoryDataService repositoryDataService)
        {
            _repositoryDataService = repositoryDataService;
        }

        [HttpGet]
        public async Task<HttpContent> GetDataAsync(string file = null)
        {
            return new StringContent(await _repositoryDataService.GetDataAsync(file), Encoding.UTF8, "application/json");
        }

        [HttpPut]
        public Task UpdateAsync(string json, string userName, string ipAddress, string file = null)
        {
            return _repositoryDataService.UpdateAsync(json, userName, ipAddress, file);
        }

        [HttpGet("existing")]
        public Task<List<string>> GetExistingFileNamesAsync()
        {
            return _repositoryDataService.GetExistingFileNamesAsync();
        }

        [HttpGet("exists")]
        public Task<bool> ExistsAsync(string file = null)
        {
            return _repositoryDataService.ExistsAsync(file);
        }

        [HttpDelete]
        public Task DeleteAsync(string file)
        {
            return _repositoryDataService.DeleteAsync(file);
        }
    }
}