using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.KeyValue
{
    public class KeyValueHistoricEntity : TableEntity
    {
        private string _dateTime;

        public string GeneratePartitionKey() => KeyValueId;

        public string DateTime
        {
            get => _dateTime ?? RowKey;
            set => _dateTime = value;
        }

        public string KeyValueId { get; set; }
        public string NewValue { get; set; }
        public string NewOverride { get; set; }
        public string KeyValuesSnapshot { get; set; }
        public string UserName { get; set; }
        public string UserIpAddress { get; set; }
    }
}