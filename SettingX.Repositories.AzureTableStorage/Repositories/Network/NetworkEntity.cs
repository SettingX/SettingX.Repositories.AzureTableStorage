using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace SettingX.Repositories.AzureTableStorage.Repositories.Network
{
    public class NetworkEntity : TableEntity
    {
        public string Id => RowKey;
        public string Name { get; set; }
        public string Ip { get; set; }

        internal static string GeneratePartitionKey() => "Network";
        internal static string GenerateRowKey(string id) => id;

        internal static NetworkEntity Create(SettingX.Core.Models.Network src) => new NetworkEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = src.Id ?? Guid.NewGuid().ToString(),
            Name = src.Name,
            Ip = src.Ip
        };
        
        internal static SettingX.Core.Models.Network ToDomain(NetworkEntity src) => new SettingX.Core.Models.Network
        {
            Id = src.Id,
            Name = src.Name,
            Ip = src.Ip
        };
    }
}