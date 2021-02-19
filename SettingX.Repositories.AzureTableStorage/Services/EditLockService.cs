using System;
using System.Threading.Tasks;
using SettingX.Core.Models;
using SettingX.Core.Repositories;

namespace SettingX.Repositories.AzureTableStorage.Services
{
    public class EditLockService
    {
        private readonly IEditLockRepository _editLockRepository;

        public EditLockService(IEditLockRepository editLockRepository)
        {
            _editLockRepository = editLockRepository;
        }
        
        public Task<EditLock> GetJsonPageLockAsync()
        {
            return _editLockRepository.GetJsonPageLockAsync();
        }

        public Task SetJsonPageLockAsync(string userEmail, string userName, string ipAddress)
        {
            return _editLockRepository.SetJsonPageLockAsync(userEmail, userName, ipAddress);
        }

        public Task ResetJsonPageLockAsync()
        {
            return _editLockRepository.ResetJsonPageLockAsync();
        }
    }
}