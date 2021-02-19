using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class AccountTokenHistoryService : IAccountTokenHistoryService
    {
        public Task SaveTokenHistoryAsync(Token token, string userName, string userIpAddress)
        {
            throw new System.NotImplementedException();
        }
    }
}