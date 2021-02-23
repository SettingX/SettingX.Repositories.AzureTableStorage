using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.ServiceToken
{
    public class ServiceTokenEntity : TableEntity
    {
        private string _token;

        public static string GeneratePartitionKey() => "S";

        public string Token
        {
            get => _token ?? RowKey;
            set => _token = value;
        }
        public string SecurityKeyOne { get; set; }
        public string SecurityKeyTwo { get; set; }
    }
}