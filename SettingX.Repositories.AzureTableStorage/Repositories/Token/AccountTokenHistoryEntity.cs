using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Token
{
    public class AccountTokenHistoryEntity : TableEntity
    {
        public string TokenId { get; set; }

        public string UserName { get; set; }
        public string AccessList { get; set; }
        public string IpList { get; set; }
        public string UserIpAddress { get; set; }
    }
}