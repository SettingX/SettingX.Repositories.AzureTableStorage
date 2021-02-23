using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IServiceTokensHistoryService
    {
        Task SaveTokenHistoryAsync(ServiceToken token, string userName, string userIpAddress);
    }
}