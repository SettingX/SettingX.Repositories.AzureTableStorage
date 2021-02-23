using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Token
{
    public class TokensRepository : ITokensRepository
    {
        private readonly INoSQLTableStorage<TokenEntity> _tableStorage;
        private readonly IMapper _mapper;

        public TokensRepository(INoSQLTableStorage<TokenEntity> tableStorage, IMapper mapper)
        {
            _tableStorage = tableStorage;
            _mapper = mapper;
        }

        public async Task<SettingX.Core.Models.Token> GetAsync(string tokenId)
        {
            var pk = TokenEntity.GeneratePartitionKey();
            var rk = TokenEntity.GenerateRowKey(tokenId);

            return _mapper.Map<SettingX.Core.Models.Token>(await _tableStorage.GetDataAsync(pk, rk));
        }

        public async Task<SettingX.Core.Models.Token> GetTopRecordAsync()
        {
            var pk = TokenEntity.GeneratePartitionKey();
            var result = await _tableStorage.GetTopRecordAsync(pk);
            return _mapper.Map<SettingX.Core.Models.Token>(result);
        }

        public async Task<List<SettingX.Core.Models.Token>> GetAllAsync()
        {
            var pk = TokenEntity.GeneratePartitionKey();
            return _mapper.Map<List<SettingX.Core.Models.Token>>(await _tableStorage.GetDataAsync(pk));
        }

        public async Task RemoveTokenAsync(string tokenId)
        {
            var pk = TokenEntity.GeneratePartitionKey();
            await _tableStorage.DeleteAsync(pk, tokenId);
        }

        public async Task SaveTokenAsync(SettingX.Core.Models.Token token)
        {
            var pk = TokenEntity.GeneratePartitionKey();
            var rk = TokenEntity.GenerateRowKey(token.TokenId);
            
            var ts = await _tableStorage.GetDataAsync(pk, rk)
                 ?? new TokenEntity { PartitionKey = pk, RowKey = rk };

            ts.TokenId = token.TokenId;
            ts.AccessList = token.AccessList;
            ts.IpList = token.IpList;

            await _tableStorage.InsertOrMergeAsync(ts);
        }
    }
}