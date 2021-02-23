using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Repository
{
    public class RepositoriesRepository : IRepositoriesRepository
    {
        private readonly INoSQLTableStorage<RepositoryEntity> _tableStorage;
        private readonly IMapper _mapper;

        public RepositoriesRepository(INoSQLTableStorage<RepositoryEntity> tableStorage, IMapper mapper)
        {
            _tableStorage = tableStorage;
            _mapper = mapper;
        }

        public async Task<SettingX.Core.Models.Repository> GetAsync(string repositoryId)
        {
            var pk = RepositoryEntity.GeneratePartitionKey();
            var rk = RepositoryEntity.GenerateRowKey(repositoryId);

            return _mapper.Map<SettingX.Core.Models.Repository>(await _tableStorage.GetDataAsync(pk, rk));
        }

        public async Task<List<SettingX.Core.Models.Repository>> GetAllAsync()
        {
            var pk = RepositoryEntity.GeneratePartitionKey();
            return _mapper.Map<List<SettingX.Core.Models.Repository>>(await _tableStorage.GetDataAsync(pk));
        }

        public async Task RemoveRepositoryAsync(string repositoryId)
        {
            var pk = RepositoryEntity.GeneratePartitionKey();
            await _tableStorage.DeleteAsync(pk, repositoryId);
        }

        public async Task SaveRepositoryAsync(SettingX.Core.Models.Repository repository)
        {
            var pk = RepositoryEntity.GeneratePartitionKey();
            var rk = RepositoryEntity.GenerateRowKey(repository.RepositoryId);

            var rs = await _tableStorage.GetDataAsync(pk, rk)
                 ?? new RepositoryEntity { PartitionKey = pk, RowKey = rk };

            rs.Name = repository.Name;
            rs.GitUrl = repository.GitUrl;
            rs.Branch = repository.Branch;
            rs.FileName = repository.FileName;
            rs.UserName = repository.UserName;
            rs.ConnectionUrl = repository.ConnectionUrl;
            rs.UseManualSettings = repository.UseManualSettings;
            rs.Tag = repository.Tag;

            await _tableStorage.InsertOrMergeAsync(rs);
        }
    }
}