using System;
using Microsoft.Extensions.Configuration;

namespace SettingX.Repositories.AzureTableStorage.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string SecretsConnectionString { set; get; }
        public string UserConnectionString { set; get; }

        public static AppSettings Create(IConfiguration configuration)
        {
            return new AppSettings
            {
                ConnectionString = configuration["ConnectionString"] ?? throw new ArgumentNullException(nameof(ConnectionString)),
                SecretsConnectionString = configuration["SecretsConnectionString"] ?? throw new ArgumentNullException(nameof(UserConnectionString)),
                UserConnectionString = configuration["UserConnectionString"] ?? throw new ArgumentNullException(nameof(SecretsConnectionString))
            };
        }
    }
}