using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Lock
{
    public class EditLockEntity : TableEntity
    {
        public static string GeneratePartitionKey() => "L";

        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string IpAddress { get; set; }
    }
}