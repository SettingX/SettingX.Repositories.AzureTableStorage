using System.Collections.Generic;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;
using SettingX.Repositories.AzureTableStorage.Core.Services;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRoleRepository _rolesRepository;

        public RolesService(IRoleRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public Task<Role> GetAsync(string roleId)
        {
            return _rolesRepository.GetAsync(roleId);
        }

        public Task<Role> GetByNameAsync(string roleName)
        {
            return _rolesRepository.GetByNameAsync(roleName);
        }

        public Task<List<Role>> GetAllAsync()
        {
            return _rolesRepository.GetAllAsync();
        }

        public Task<List<Role>> FindAsync(List<string> roleIds)
        {
            return _rolesRepository.FindAsync(roleIds);
        }

        public Task SaveAsync(Role entity)
        {
            return _rolesRepository.SaveAsync(entity);
        }

        public Task RemoveAsync(string roleId)
        {
            return _rolesRepository.RemoveAsync(roleId);
        }
    }
}