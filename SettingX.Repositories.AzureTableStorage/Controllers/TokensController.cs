using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettingX.Core.Models;
using SettingX.Repositories.AzureTableStorage.Core.Services;
using SettingX.Repositories.Common.Client.Api;

namespace SettingX.Repositories.AzureTableStorage.Controllers
{
    [Route("api/tokens")]
    public class TokensController : ControllerBase, ITokensApi
    {
        private readonly ITokensService _tokensService;

        public TokensController(ITokensService tokensService)
        {
            _tokensService = tokensService;
        }

        [HttpGet("{tokenId}")]
        public Task<Token> GetAsync([FromRoute] string tokenId)
        {
            return _tokensService.GetAsync(tokenId);
        }

        [HttpGet("top")]
        public Task<Token> GetTopRecordAsync()
        {
            return _tokensService.GetTopRecordAsync();
        }

        [HttpGet]
        public Task<List<Token>> GetAllAsync()
        {
            return _tokensService.GetAllAsync();
        }

        [HttpDelete("{tokenId}")]
        public Task RemoveTokenAsync([FromRoute] string tokenId)
        {
            return _tokensService.RemoveTokenAsync(tokenId);
        }

        [HttpPost]
        public Task SaveTokenAsync(Token token)
        {
            return _tokensService.SaveTokenAsync(token);
        }
    }
}