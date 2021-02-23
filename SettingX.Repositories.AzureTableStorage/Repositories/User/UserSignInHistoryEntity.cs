using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.User
{
    public class UserSignInHistoryEntity : TableEntity
    {
        public static string GeneratePartitionKey() => "UH";

        public string GetRawKey()
        {
            return SignInDate.StorageString();
        }

        public string UserEmail { get; set; }

        public DateTime SignInDate { get; set; }

        public string IpAddress { get; set; }
    }
}