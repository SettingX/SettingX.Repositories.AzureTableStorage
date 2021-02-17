using System;
using Microsoft.Extensions.Configuration;

namespace SettingX.Repositories.AzureTableStorage.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public static AppSettings Create(IConfiguration configuration)
        {
            return new AppSettings
            {
                ConnectionString = configuration["ConnectionString"] ?? throw new ArgumentNullException(nameof(ConnectionString))
            };
        }
    }
}