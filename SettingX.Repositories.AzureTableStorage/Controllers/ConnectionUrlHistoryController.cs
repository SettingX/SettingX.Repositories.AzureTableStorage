using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/connection_url_history")]
    public class ConnectionUrlHistoryController : ControllerBase, IConnectionUrlHistoryApi
    {
        private readonly IConnectionUrlHistoryService _connectionUrlHistoryService;

        public ConnectionUrlHistoryController(IConnectionUrlHistoryService connectionUrlHistoryService)
        {
            _connectionUrlHistoryService = connectionUrlHistoryService;
        }

        [HttpGet]
        public Task<(List<ConnectionUrlHistoricEvent>, int)> GetPageAsync([FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            return _connectionUrlHistoryService.GetPageAsync(pageNum, pageSize);
        }

        [HttpGet("{repositoryId}")]
        public Task<List<ConnectionUrlHistoricEvent>> GetByRepositoryIdAsync([FromRoute] string repositoryId)
        {
            return _connectionUrlHistoryService.GetByRepositoryIdAsync(repositoryId);
        }

        [HttpPost("{repositoryId}")]
        public Task SaveConnectionUrlHistoryAsync([FromRoute] string repositoryId, [FromQuery] string ip, [FromQuery] string userAgent)
        {
            return _connectionUrlHistoryService.SaveConnectionUrlHistoryAsync(repositoryId, ip, userAgent);
        }
    }
}