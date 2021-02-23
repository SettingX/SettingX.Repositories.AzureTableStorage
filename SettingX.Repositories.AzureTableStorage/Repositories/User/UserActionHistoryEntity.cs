using System;
using Microsoft.WindowsAzure.Storage.Table;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Repositories.User
{
    public class UserActionHistoryEntity : TableEntity
    {
        public static string GeneratePartitionKey() => "UAH";
        public string GetRawKey() => ActionDate.StorageString();

        public string UserEmail { get; set; }
        public DateTime ActionDate { get; set; }
        public string IpAddress { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Params { get; set; }

        public static UserActionHistoryEntity Create(UserActionHistory entity)
        {
            return new UserActionHistoryEntity
            {
                UserEmail = entity.UserEmail,
                ActionDate = entity.ActionDate,
                IpAddress = entity.IpAddress,
                ControllerName = entity.ControllerName,
                ActionName = entity.ActionName,
                Params = entity.Params,
                PartitionKey = GeneratePartitionKey(),
                RowKey = entity.ActionDate.StorageString()
            };
        }
    }
}