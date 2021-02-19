using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Microsoft.WindowsAzure.Storage.Table;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.KeyValue
{
    public class KeyValuesRepository : IKeyValuesRepository
    {
        private readonly INoSQLTableStorage<KeyValueEntity> _tableStorage;
        private readonly IKeyValueHistoryRepository _history;
        private readonly IMapper _mapper;

        public KeyValuesRepository(INoSQLTableStorage<KeyValueEntity> tableStorage, IKeyValueHistoryRepository history, IMapper mapper)
        {
            _tableStorage = tableStorage;
            _history = history;
            _mapper = mapper;
        }

        public async Task<Dictionary<string, SettingX.Core.Models.KeyValue>> GetAsync()
        {
            var entries = await GetKeyValuesAsync();
            return entries.ToDictionary(itm => itm.KeyValueId, itm => itm);
        }

        public async Task<SettingX.Core.Models.KeyValue> GetTopRecordAsync()
        {
            var pk = KeyValueEntity.GeneratePartitionKey();
            var result = await _tableStorage.GetTopRecordAsync(pk);
            return _mapper.Map<SettingX.Core.Models.KeyValue>(result);
        }

        public async Task<List<SettingX.Core.Models.KeyValue>> GetAsync(string id, string type)
        {
            var pk = KeyValueEntity.GeneratePartitionKey();

            Func<KeyValueEntity, bool> selector = e => true;

            if (!string.IsNullOrWhiteSpace(type))
                selector = e => e.Types != null && e.Types.Contains(type);

            if (!string.IsNullOrWhiteSpace(id))
                selector = e => e.KeyValueId == id;
            
            var list = await _tableStorage.GetDataAsync(pk, selector);
            
            return _mapper.Map<List<SettingX.Core.Models.KeyValue>>(list);
        }

        public async Task<List<SettingX.Core.Models.KeyValue>> GetKeyValuesAsync()
        {
            var pk = KeyValueEntity.GeneratePartitionKey();
            return _mapper.Map<List<SettingX.Core.Models.KeyValue>>(await _tableStorage.GetDataAsync(pk));
        }

        public async Task<List<SettingX.Core.Models.KeyValue>> GetKeyValuesAsync(string keyRepoName, string filter, string search, string repositoryId = null)
        {
            Func<KeyValueEntity, bool> selector = e => true;

            if (!string.IsNullOrWhiteSpace(keyRepoName))
                selector = x => x.RepositoryNames != null && x.RepositoryNames.Contains(keyRepoName);

            if (!string.IsNullOrWhiteSpace(search))
                selector = x => FilterKeyValue(x, search);
            
            if(!string.IsNullOrWhiteSpace(filter))
                selector = x => FilterKeyValue(x, filter, search);
            
            string queryText = TableQuery.GenerateFilterCondition(nameof(KeyValueEntity.PartitionKey), QueryComparisons.Equal, KeyValueEntity.GeneratePartitionKey());
            if (!string.IsNullOrWhiteSpace(repositoryId))
            {
                string repositoryFilter = TableQuery.GenerateFilterCondition(nameof(KeyValueEntity.RepositoryId), QueryComparisons.Equal, repositoryId);
                queryText = TableQuery.CombineFilters(queryText, TableOperators.And, repositoryFilter);
            }
            var query = new TableQuery<KeyValueEntity>().Where(queryText);
            
            return _mapper.Map<List<SettingX.Core.Models.KeyValue>>(await _tableStorage.WhereAsync(query, selector));
        }
        
        private bool FilterKeyValue(KeyValueEntity entity, string filter, string search)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                if (entity.RepositoryNames == null)
                    return false;

                if (!entity.RepositoryNames.Select(repo => repo.ToLower()).Contains(filter))
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                if (entity.KeyValueId.ToLower().Contains(search)
                    || !string.IsNullOrWhiteSpace(entity.Value) && entity.Value.ToLower().Contains(search)
                    || entity.Override != null && string.Join("", entity.Override.Select(x => x.Value?.ToLower() ?? string.Empty)).Contains(search))
                    return true;
                return false;
            }

            return true;
        }

        public async Task<SettingX.Core.Models.KeyValue> GetKeyValueAsync(string key)
        {
            return _mapper.Map<SettingX.Core.Models.KeyValue>(await _tableStorage.GetDataAsync(KeyValueEntity.GeneratePartitionKey(), KeyValueEntity.GenerateRowKey(key)));
        }

        public async Task<Dictionary<string, SettingX.Core.Models.KeyValue>> GetKeyValuesAsync(List<string> keys)
        {
            var items = await _tableStorage.GetDataAsync(KeyValueEntity.GeneratePartitionKey(), keys.Select(KeyValueEntity.GenerateRowKey));
            return items.ToDictionary(i => i.RowKey, i => _mapper.Map<SettingX.Core.Models.KeyValue>(i));
        }

        public async Task<bool> UpdateKeyValueAsync(List<SettingX.Core.Models.KeyValue> keyValueList)
        {
            if (!keyValueList.Any())
                return true;

            var list = new List<KeyValueEntity>();
            foreach (var item in keyValueList)
            {
                //var kv = item as KeyValueEntity;
                //list.Add(kv ?? new KeyValueEntity(item));
                list.Add(new KeyValueEntity(item));
            }

            try
            {
                await _tableStorage.InsertOrMergeBatchAsync(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> ReplaceKeyValueAsync(List<SettingX.Core.Models.KeyValue> keyValueList)
        {
            if (!keyValueList.Any())
                return true;

            var list = new List<KeyValueEntity>();
            foreach (var item in keyValueList)
            {
                //var kv = item as KeyValueEntity;
                //list.Add(kv ?? new KeyValueEntity(item));
                list.Add(new KeyValueEntity(item));
            }

            try
            {
                await _tableStorage.InsertOrReplaceBatchAsync(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task RemoveNetworkOverridesAsync(string networkId)
        {
            var kvs = await GetKeyValuesAsync();
            var keyValues = kvs.Where(item => item.Override != null && item.Override.Any(o => o.NetworkId == networkId));

            var keyValuesToUpdate = new List<SettingX.Core.Models.KeyValue>();

            foreach (var keyValue in keyValues)
            {
                var list = keyValue.Override.ToList();
                var overrideValue = list.FirstOrDefault(item => item.NetworkId == networkId);

                if (overrideValue != null)
                {
                    list.Remove(overrideValue);
                    keyValue.Override = list.ToArray();
                    keyValuesToUpdate.Add(keyValue);
                }
            }

            await UpdateKeyValueAsync(keyValuesToUpdate);
        }

        public async Task DeleteKeyValueWithHistoryAsync(string keyValueId, string description, string userName, string userIpAddress)
        {
            var kvItem = await _tableStorage.GetDataAsync(KeyValueEntity.GeneratePartitionKey(), keyValueId);
            if (kvItem != null)
            {
                await _tableStorage.DeleteAsync(kvItem);
                await _history.DeleteKeyValueHistoryAsync(keyValueId, description, userName, userIpAddress);
            }
        }
        
        private bool FilterKeyValue(KeyValueEntity entity, string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                if (entity.KeyValueId.ToLower().Contains(search)
                    || !string.IsNullOrWhiteSpace(entity.Value) && entity.Value.ToLower().Contains(search)
                    || entity.Override != null && string.Join("", entity.Override.Select(x => x.Value?.ToLower() ?? string.Empty)).Contains(search))
                    return true;
                return false;
            }

            return true;
        }
    }
}