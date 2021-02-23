using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IUsersService
    {
        Task<User> GetUserByUserEmailAsync(string userEmail, string passwordHash);
        Task CreateInitialAdminAsync(string defaultUserEmail, string defaultUserPasswordHash);
        Task CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<List<User>> GetUsersAsync();
        Task<User> GetTopUserRecordAsync();
        Task<bool> RemoveUserAsync(string userEmail);
    }
}