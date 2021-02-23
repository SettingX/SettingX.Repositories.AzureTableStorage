using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IUserSignInHistoryService
    {
        Task SaveUserLoginAsync(User user, string userIpAddress);
    }
}