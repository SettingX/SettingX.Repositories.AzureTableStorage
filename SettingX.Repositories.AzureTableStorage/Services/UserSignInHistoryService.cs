using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class UserSignInHistoryService : IUserSignInHistoryService
    {
        private readonly IUserSignInHistoryRepository _userSignInHistoryRepository;

        public UserSignInHistoryService(IUserSignInHistoryRepository userSignInHistoryRepository)
        {
            _userSignInHistoryRepository = userSignInHistoryRepository;
        }

        public Task SaveUserLoginAsync(User user, string userIpAddress)
        {
            return _userSignInHistoryRepository.SaveUserLoginHistoryAsync(user, userIpAddress);
        }
    }
}