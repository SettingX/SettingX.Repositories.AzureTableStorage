using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class UserActionsHistoryService : IUserActionsHistoryService
    {
        private readonly IUserActionHistoryRepository _userActionHistoryRepository;

        public UserActionsHistoryService(IUserActionHistoryRepository userActionHistoryRepository)
        {
            _userActionHistoryRepository = userActionHistoryRepository;
        }

        public Task SaveUserActionHistoryAsync(UserActionHistory userActionHistory)
        {
            return _userActionHistoryRepository.SaveUserActionHistoryAsync(userActionHistory);
        }
    }
}