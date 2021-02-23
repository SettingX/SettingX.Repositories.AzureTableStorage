using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IUserActionsHistoryService
    {
        Task SaveUserActionHistoryAsync(UserActionHistory userActionHistory);
    }
}