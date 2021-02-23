using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.ServiceToken
{
    public class ServiceTokenHistoryEntity : TableEntity
    {
        public string TokenId { get; set; }
        public string UserName { get; set; }
        public string KeyOne { get; set; }
        public string KeyTwo { get; set; }
        public string UserIpAddress { get; set; }
    }
}