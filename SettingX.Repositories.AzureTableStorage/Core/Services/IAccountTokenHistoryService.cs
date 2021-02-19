using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IAccountTokenHistoryService
    {
        Task SaveTokenHistoryAsync(Token token, string userName, string userIpAddress);
    }
}