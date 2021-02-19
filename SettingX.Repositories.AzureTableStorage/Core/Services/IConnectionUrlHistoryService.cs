using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Core.Services
{
    public interface IConnectionUrlHistoryService
    {
        Task<(List<ConnectionUrlHistoricEvent>, int)> GetPageAsync(int pageNum, int pageSize);
        Task<List<ConnectionUrlHistoricEvent>> GetByRepositoryIdAsync(string repositoryId);
        Task SaveConnectionUrlHistoryAsync(
            string repositoryId,
            string ip,
            string userAgent);
    }
}