using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> GetUserByUserEmailAsync(string userEmail, string passwordHash)
        {
            return _userRepository.GetUserByUserEmailAsync(userEmail, passwordHash);
        }

        public Task CreateInitialAdminAsync(string defaultUserEmail, string defaultUserPasswordHash)
        {
            return _userRepository.CreateInitialAdminAsync(defaultUserEmail, defaultUserPasswordHash);
        }

        public Task CreateUserAsync(User user)
        {
            return _userRepository.CreateUserAsync(user);
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            return _userRepository.UpdateUserAsync(user);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _userRepository.GetUsersAsync();
        }

        public Task<User> GetTopUserRecordAsync()
        {
            return _userRepository.GetTopUserRecordAsync();
        }

        public Task<bool> RemoveUserAsync(string userEmail)
        {
            return _userRepository.RemoveUserAsync(userEmail);
        }
    }
}