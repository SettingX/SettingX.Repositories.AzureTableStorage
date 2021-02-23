using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Repository
{
    public class RepositoryUpdateHistoryEntity : TableEntity
    {
        private string _repositoryId;
        private DateTimeOffset? _createdAt;

        public static string GeneratePartitionKey() => "RUH";

        public static string GenerateRowKey(string repositoryUpdateHistory) => repositoryUpdateHistory;

        public string InitialCommit { get; set; }
        public string User { get; set; }
        public string Branch { get; set; }
        public bool IsManual { get; set; }
        public DateTimeOffset CreatedAt
        {
            get => _createdAt ?? Timestamp;
            set => _createdAt = value;
        }

        public string RepositoryId
        {
            get { return _repositoryId ?? RowKey; }
            set { _repositoryId = value; }
        }
    }
}