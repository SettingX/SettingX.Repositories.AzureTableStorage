using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Microsoft.WindowsAzure.Storage.Table;
using SettingX.Core.Models;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Repository
{
    public class ConnectionUrlHistoryRepository : IConnectionUrlHistoricEventsRepository
    {
        private readonly INoSQLTableStorage<ConnectionUrlHistoricEntity> _tableStorage;
        private readonly IMapper _mapper;

        private int? _totalCount;
        private Task _totalCountTask;

        public ConnectionUrlHistoryRepository(
            INoSQLTableStorage<ConnectionUrlHistoricEntity> tableStorage,
            IMapper mapper)
        {
            _tableStorage = tableStorage;
            _mapper = mapper;
        }

        public async Task<(List<ConnectionUrlHistoricEvent>, int)> GetPageAsync(int pageNum, int pageSize)
        {
            var pk = ConnectionUrlHistoricEntity.GeneratePartitionKey();
            var filter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, pk);
            var query =  new TableQuery<ConnectionUrlHistoricEntity>().Where(filter);
            var pageItems = new List<ConnectionUrlHistoricEntity>();
            int batchCount = -1;
            int count = 0;
            int skip = (pageNum - 1) * pageSize;
            await _tableStorage.ExecuteAsync(
                query,
                batch => {
                    batchCount = batch.Count();
                    if (count + batchCount < skip)
                        return;
                    foreach(var item in batch)
                    {
                        if (count < skip)
                        {
                            ++count;
                            continue;
                        }
                        if (pageItems.Count == pageSize)
                            return;
                        pageItems.Add(item);
                    }
                    count += batchCount;
                },
                () => batchCount > 0 && pageItems.Count < pageSize);

            var totalCount = 10 * pageSize;
            if (_totalCount.HasValue)
                totalCount = _totalCount.Value;
            else if (_totalCountTask == null)
                Task.Run(async () =>
                {
                    _totalCountTask = CalculateTotalCountAsync();
                    await _totalCountTask;
                });
            return (_mapper.Map<List<ConnectionUrlHistoricEvent>>(pageItems), totalCount);
        }

        public async Task<List<ConnectionUrlHistoricEvent>> GetByRepositoryIdAsync(string repositoryId)
        {
            var list = await _tableStorage.GetDataAsync(repositoryId);
            if (!list.Any())
            {
                var pk = ConnectionUrlHistoricEntity.GeneratePartitionKey();
                list = await _tableStorage.GetDataAsync(pk, x => x.RepositoryId == repositoryId);
                foreach(var item in list)
                {
                    item.PartitionKey = item.RepositoryId;
                }
                await _tableStorage.InsertOrMergeBatchAsync(list);
            }

            return _mapper.Map<List<ConnectionUrlHistoricEvent>>(list);
        }

        public async Task SaveConnectionUrlHistoricEventAsync(string repositoryId, string ip, string userAgent)
        {
            var id = Guid.NewGuid().ToString();
            var commonPk = new ConnectionUrlHistoricEntity
            {
                PartitionKey = ConnectionUrlHistoricEntity.GeneratePartitionKey(),
                RowKey = id,
                RepositoryId = repositoryId,
                Ip = ip,
                UserAgent = userAgent,
            };
            var repoPk = new ConnectionUrlHistoricEntity
            {
                PartitionKey = repositoryId,
                RowKey = id,
                RepositoryId = repositoryId,
                Ip = ip,
                UserAgent = userAgent,
            };

            var tasks = new List<Task>
            {
                _tableStorage.InsertOrMergeAsync(commonPk),
                _tableStorage.InsertOrMergeAsync(repoPk),
            };
            await Task.WhenAll(tasks);

            if (_totalCount.HasValue)
                _totalCount = _totalCount.Value + 1;
        }

        private async Task CalculateTotalCountAsync()
        {
            if (_totalCountTask != null)
                return;
            try
            {
                var pk = ConnectionUrlHistoricEntity.GeneratePartitionKey();
                int totalCount = 0;
                await _tableStorage.GetDataByChunksAsync(pk, c =>
                {
                    totalCount += c.Count();
                });
                _totalCount = totalCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _totalCountTask = null;
            }
        }
    }
}