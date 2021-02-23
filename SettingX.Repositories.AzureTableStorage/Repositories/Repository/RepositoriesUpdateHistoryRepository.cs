using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Microsoft.WindowsAzure.Storage.Table;
using SettingX.Core.Models;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Repository
{
    public class RepositoriesUpdateHistoryRepository : IRepositoryUpdateHistoryEventsRepository
    {
        private readonly INoSQLTableStorage<RepositoryUpdateHistoryEntity> _tableStorage;
        private readonly IMapper _mapper;

        public RepositoriesUpdateHistoryRepository(INoSQLTableStorage<RepositoryUpdateHistoryEntity> tableStorage, IMapper mapper)
        {
            _tableStorage = tableStorage;
            _mapper = mapper;
        }
        
        public async Task<RepositoryUpdateHistoricEvent> GetAsync(string repositoryUpdateHistoryId)
        {
            var pk = RepositoryUpdateHistoryEntity.GeneratePartitionKey();
            var rk = RepositoryUpdateHistoryEntity.GenerateRowKey(repositoryUpdateHistoryId);

            return _mapper.Map<RepositoryUpdateHistoricEvent>(await _tableStorage.GetDataAsync(pk, rk));
        }
        
        public async Task RemoveRepositoryUpdateHistoryAsync(List<string> repositoryUpdateHistoryIds)
        {
            foreach (var id in repositoryUpdateHistoryIds)
            {
                var pk = RepositoryUpdateHistoryEntity.GeneratePartitionKey();
                var rk = RepositoryUpdateHistoryEntity.GenerateRowKey(id);
                await _tableStorage.DeleteAsync(pk, rk);
            }
        }

        public async Task SaveRepositoryUpdateHistoryAsync(RepositoryUpdateHistoricEvent entity)
        {
            var pk = RepositoryUpdateHistoryEntity.GeneratePartitionKey();
            var rk = RepositoryUpdateHistoryEntity.GenerateRowKey(entity.RepositoryId);

            var ruh = await _tableStorage.GetDataAsync(pk, rk)
                      ?? new RepositoryUpdateHistoryEntity { PartitionKey = pk, RowKey = rk };

            ruh.RepositoryId = entity.RepositoryId;
            ruh.InitialCommit = entity.InitialCommit;
            ruh.User = entity.User;
            ruh.Branch = entity.Branch;
            ruh.IsManual = entity.IsManual;
            ruh.CreatedAt = entity.CreatedAt;

            await _tableStorage.InsertOrMergeAsync(ruh);
        }

        public async Task<List<RepositoryUpdateHistoricEvent>> GetByInitialCommitAsync(string initialCommit)
        {
            var partitionFilter = TableQuery.GenerateFilterCondition(nameof(RepositoryUpdateHistoryEntity.PartitionKey), QueryComparisons.Equal, RepositoryUpdateHistoryEntity.GeneratePartitionKey());
            var repositoryFilter = TableQuery.GenerateFilterCondition(nameof(RepositoryUpdateHistoryEntity.InitialCommit), QueryComparisons.Equal, initialCommit);
            
            var queryText = TableQuery.CombineFilters(partitionFilter, TableOperators.And, repositoryFilter);
            
            var query = new TableQuery<RepositoryUpdateHistoryEntity>().Where(queryText);
            
            return _mapper.Map<List<RepositoryUpdateHistoricEvent>>(await _tableStorage.WhereAsync(query));
        }
    }
}