using Microsoft.WindowsAzure.Storage.Table;
using SettingX.Core.Models;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Repository
{
    public class ConnectionUrlHistoricEntity : TableEntity
    {
        private string _id;
        private string _datetime;

        public static string GeneratePartitionKey() => "CUH";

        public static string GenerateRowKey(string connectionUrlHistoryId) => connectionUrlHistoryId;

        public string Id
        {
            get => _id ?? RowKey;
            set => _id = value;
        }

        public string Ip { get; set; }
        public string RepositoryId { get; set; }
        public string UserAgent { get; set; }

        public string Datetime
        {
            get => _datetime ?? Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            set => _datetime = value;
        }
    }
}