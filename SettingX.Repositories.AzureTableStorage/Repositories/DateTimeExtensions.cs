using System;

namespace SettingX.Repositories.AzureTableStorage.Repositories
{
    public static class DateTimeExtensions
    {
        internal static string StorageString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}