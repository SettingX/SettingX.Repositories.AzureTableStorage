using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class ServiceTokensHistoryService : IServiceTokensHistoryService
    {
        private readonly IServiceTokenHistoryRepository _serviceTokenHistoryRepository;

        public ServiceTokensHistoryService(IServiceTokenHistoryRepository serviceTokenHistoryRepository)
        {
            _serviceTokenHistoryRepository = serviceTokenHistoryRepository;
        }

        public Task SaveTokenHistoryAsync(ServiceToken token, string userName, string userIpAddress)
        {
            return _serviceTokenHistoryRepository.SaveTokenHistoryAsync(token, userName, userIpAddress);
        }
    }
}