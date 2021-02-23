using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class ServiceTokensService : IServiceTokensService
    {
        private readonly IServiceTokenRepository _serviceTokenRepository;

        public ServiceTokensService(IServiceTokenRepository serviceTokenRepository)
        {
            _serviceTokenRepository = serviceTokenRepository;
        }

        public Task<List<ServiceToken>> GetAllAsync()
        {
            return _serviceTokenRepository.GetAllAsync();
        }

        public Task<ServiceToken> GetTopRecordAsync()
        {
            return _serviceTokenRepository.GetTopRecordAsync();
        }

        public Task<ServiceToken> GetAsync(string tokenKey)
        {
            return _serviceTokenRepository.GetAsync(tokenKey);
        }

        public Task<bool> SaveOrUpdateAsync(ServiceToken token)
        {
            return _serviceTokenRepository.SaveOrUpdateAsync(token);
        }

        public Task<bool> RemoveAsync(string tokenId)
        {
            return _serviceTokenRepository.RemoveAsync(tokenId);
        }
    }
}