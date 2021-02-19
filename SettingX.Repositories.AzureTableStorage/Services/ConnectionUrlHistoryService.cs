using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class ConnectionUrlHistoryService : IConnectionUrlHistoryService
    {
        private readonly IConnectionUrlHistoricEventsRepository _connectionUrlHistoricEventsRepository;

        public ConnectionUrlHistoryService(IConnectionUrlHistoricEventsRepository connectionUrlHistoricEventsRepository)
        {
            _connectionUrlHistoricEventsRepository = connectionUrlHistoricEventsRepository;
        }

        public Task<(List<ConnectionUrlHistoricEvent>, int)> GetPageAsync(int pageNum, int pageSize)
        {
            return _connectionUrlHistoricEventsRepository.GetPageAsync(pageNum, pageSize);
        }

        public Task<List<ConnectionUrlHistoricEvent>> GetByRepositoryIdAsync(string repositoryId)
        {
            return _connectionUrlHistoricEventsRepository.GetByRepositoryIdAsync(repositoryId);
        }

        public Task SaveConnectionUrlHistoryAsync(string repositoryId, string ip, string userAgent)
        {
            return _connectionUrlHistoricEventsRepository.SaveConnectionUrlHistoricEventAsync(repositoryId, ip, userAgent);
        }
    }
}