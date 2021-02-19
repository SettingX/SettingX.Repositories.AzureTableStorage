using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IEditLockService
    {
        Task<EditLock> GetJsonPageLockAsync();
        Task SetJsonPageLockAsync(string userEmail, string userName, string ipAddress);
        Task ResetJsonPageLockAsync();
    }
}