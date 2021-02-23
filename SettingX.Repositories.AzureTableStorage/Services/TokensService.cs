using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class TokensService : ITokensService
    {
        private readonly ITokensRepository _tokensRepository;

        public TokensService(ITokensRepository tokensRepository)
        {
            _tokensRepository = tokensRepository;
        }

        public Task<Token> GetAsync(string tokenId)
        {
            return _tokensRepository.GetAsync(tokenId);
        }

        public Task<Token> GetTopRecordAsync()
        {
            return _tokensRepository.GetTopRecordAsync();
        }

        public Task<List<Token>> GetAllAsync()
        {
            return _tokensRepository.GetAllAsync();
        }

        public Task RemoveTokenAsync(string tokenId)
        {
            return _tokensRepository.RemoveTokenAsync(tokenId);
        }

        public Task SaveTokenAsync(Token token)
        {
            return _tokensRepository.SaveTokenAsync(token);
        }
    }
}