using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Repository
{
    public class RepositoryEntity : TableEntity
    {
        private string _repositoryId;
        private string lastModified;

        public static string GeneratePartitionKey() => "R";

        public static string GenerateRowKey(string repositoryId) => repositoryId;

        public string RepositoryId
        {
            get => _repositoryId ?? RowKey;
            set => _repositoryId = value;
        }
        public string LastModified
        {
            get => lastModified ?? Timestamp.ToString("yyyy/MM/dd");
            set => lastModified = value;
        }
        public string Name { get; set; }
        public string GitUrl { get; set; }
        public string Branch { get; set; }
        public string FileName { get; set; }
        public string UserName { get; set; }
        public string ConnectionUrl { get; set; }
        public string OriginalName { get; set; }
        public bool UseManualSettings { get; set; }
        public string Tag { get; set; }
    }
}