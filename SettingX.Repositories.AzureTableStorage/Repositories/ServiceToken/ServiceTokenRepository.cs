using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.ServiceToken
{
    public class ServiceTokenRepository : IServiceTokenRepository
    {
        private readonly INoSQLTableStorage<ServiceTokenEntity> _tableStorage;
        private readonly IMapper _mapper;

        public ServiceTokenRepository(INoSQLTableStorage<ServiceTokenEntity> tableStorage, IMapper mapper)
        {
            _tableStorage = tableStorage;
            _mapper = mapper;
        }

        public async Task<List<SettingX.Core.Models.ServiceToken>> GetAllAsync()
        {
            var pk = ServiceTokenEntity.GeneratePartitionKey();
            var tokens = await _tableStorage.GetDataAsync(pk);
            return _mapper.Map<List<SettingX.Core.Models.ServiceToken>>(tokens.ToList());
        }

        public async Task<SettingX.Core.Models.ServiceToken> GetTopRecordAsync()
        {
            var pk = ServiceTokenEntity.GeneratePartitionKey();
            var result = await _tableStorage.GetTopRecordAsync(pk);
            return _mapper.Map<SettingX.Core.Models.ServiceToken>(result);
        }

        public async Task<SettingX.Core.Models.ServiceToken> GetAsync(string tokenKey)
        {
            var pk = ServiceTokenEntity.GeneratePartitionKey();
            return _mapper.Map<SettingX.Core.Models.ServiceToken>(await _tableStorage.GetDataAsync(pk, tokenKey));
        }

        public async Task<bool> SaveOrUpdateAsync(SettingX.Core.Models.ServiceToken token)
        {
            try
            {
                var pk = ServiceTokenEntity.GeneratePartitionKey();
                var sToken = await _tableStorage.GetDataAsync(pk, token.Token);
                
                if (sToken == null)
                {
                    sToken = new ServiceTokenEntity
                    {
                        PartitionKey = ServiceTokenEntity.GeneratePartitionKey(),
                        RowKey = token.Token,
                        Token = token.Token,
                    };
                }

                sToken.SecurityKeyOne = token.SecurityKeyOne;
                sToken.SecurityKeyTwo = token.SecurityKeyTwo;
                await _tableStorage.InsertOrMergeAsync(sToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveAsync(string tokenId)
        {
            try
            {
                var pk = ServiceTokenEntity.GeneratePartitionKey();
                await _tableStorage.DeleteAsync(pk, tokenId);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}