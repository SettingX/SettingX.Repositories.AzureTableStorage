using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using SettingX.Core.Models;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.User
{
    public class RolesRepository : IRoleRepository
    {
        private readonly INoSQLTableStorage<RoleEntity> _tableStorage;
        private readonly IMapper _mapper;

        public RolesRepository(INoSQLTableStorage<RoleEntity> tableStorage, IMapper mapper)
        {
            _tableStorage = tableStorage;
            _mapper = mapper;
        }

        public async Task<Role> GetAsync(string roleId)
        {
            var pk = RoleEntity.GeneratePartitionKey();
            var rk = RoleEntity.GenerateRowKey(roleId);

            return _mapper.Map<Role>(await _tableStorage.GetDataAsync(pk, rk));
        }

        public async Task<Role> GetByNameAsync(string roleName)
        {
            var pk = RoleEntity.GeneratePartitionKey();
            var list = await _tableStorage.GetDataAsync(pk, e => e.Name == roleName);
            
            return _mapper.Map<Role>(list.SingleOrDefault());
        }

        public async Task<List<Role>> GetAllAsync()
        {
            var pk = RoleEntity.GeneratePartitionKey();
            return _mapper.Map<List<Role>>(await _tableStorage.GetDataAsync(pk));
        }

        public async Task<List<Role>> FindAsync(List<string> roleIds)
        {
            var pk = RoleEntity.GeneratePartitionKey();
            var list = await _tableStorage.GetDataAsync(pk, e => roleIds.Contains(e.RoleId));
            
            return _mapper.Map<List<Role>>(list.SingleOrDefault());
        }

        public async Task SaveAsync(Role roleEntity)
        {
            var pk = RoleEntity.GeneratePartitionKey();
            var rk = RoleEntity.GenerateRowKey(roleEntity.RoleId);
            
            var role = await _tableStorage.GetDataAsync(pk, rk)
                   ?? new RoleEntity { PartitionKey = pk, RowKey = rk };

            role.RoleId = roleEntity.RoleId;
            role.Name = roleEntity.Name;
            role.KeyValues = roleEntity.KeyValues;

            await _tableStorage.InsertOrMergeAsync(role);
        }

        public async Task RemoveAsync(string roleId)
        {
            var role = await GetAsync(roleId);
            if (role != null)
            {
                await _tableStorage.DeleteAsync(RoleEntity.GeneratePartitionKey(), RoleEntity.GenerateRowKey(roleId));
            }
        }
    }
}