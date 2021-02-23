using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Repositories.User
{
    public class UsersRepository : IUserRepository
    {
        private readonly INoSQLTableStorage<UserEntity> _tableStorage;
        private readonly IMapper _mapper;

        public UsersRepository(INoSQLTableStorage<UserEntity> tableStorage, IMapper mapper)
        {
            _tableStorage = tableStorage;
            _mapper = mapper;
        }

        public async Task<SettingX.Core.Models.User> GetUserByUserEmailAsync(string userEmail, string passwordHash)
        {
            var pk = UserEntity.GeneratePartitionKey();
            var result = await _tableStorage.GetDataAsync(pk, UserEntity.GenerateRowKey(userEmail));

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                if (result == null)
                    return null;
                else
                    return _mapper.Map<SettingX.Core.Models.User>(result);
            }
            else
            {
                return result.PasswordHash.Equals(passwordHash) ? _mapper.Map<SettingX.Core.Models.User>(result) : null;
            }
        }

        public async Task CreateInitialAdminAsync(string defaultUserEmail, string defaultUserPasswordHash)
        {
            var usr = new UserEntity
            {
                PartitionKey = UserEntity.GeneratePartitionKey(),
                RowKey = defaultUserEmail,
                Email = defaultUserEmail,
                PasswordHash = defaultUserPasswordHash,
                FirstName = "Admin",
                LastName = "Initial",
                Active = true,
                Admin = true
            };
            await _tableStorage.InsertOrMergeAsync(usr);
        }

        public Task CreateUserAsync(SettingX.Core.Models.User user)
        {
            var email = UserEntity.GenerateRowKey(user.Email);
            var usr = new UserEntity
            {
                PartitionKey = UserEntity.GeneratePartitionKey(),
                RowKey = email,
                Email = email,
                PasswordHash = user.PasswordHash,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Active = user.Active,
                Admin = user.Admin,
                Roles = user.Roles,
            };

            return _tableStorage.InsertOrMergeAsync(usr);
        }

        public async Task<bool> UpdateUserAsync(SettingX.Core.Models.User user)
        {
            try
            {
                var email = UserEntity.GenerateRowKey(user.Email);
                var usr = new UserEntity
                {
                    PartitionKey = UserEntity.GeneratePartitionKey(),
                    RowKey = email,
                    Email = email,
                    PasswordHash = user.PasswordHash,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Active = user.Active,
                    Admin = user.Admin,
                    Roles = user.Roles,
                };

                await _tableStorage.InsertOrMergeAsync(usr);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<List<SettingX.Core.Models.User>> GetUsersAsync()
        {
            var pk = UserEntity.GeneratePartitionKey();
            var users = await _tableStorage.GetDataAsync(pk);
            return _mapper.Map<List<SettingX.Core.Models.User>>(users.Cast<UserEntity>().ToList());
        }

        public async Task<SettingX.Core.Models.User> GetTopUserRecordAsync()
        {
            var pk = UserEntity.GeneratePartitionKey();
            var result = await _tableStorage.GetTopRecordAsync(pk);
            return _mapper.Map<SettingX.Core.Models.User>(result);
        }

        public async Task<bool> RemoveUserAsync(string userEmail)
        {
            try
            {
                await _tableStorage.DeleteAsync(UserEntity.GeneratePartitionKey(), UserEntity.GenerateRowKey(userEmail));
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}