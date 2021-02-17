using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace SettingX.Repositories.AzureTableStorage.Settings
{
    internal static class SettingsLoadExtensions
    {
        internal static IConfigurationBuilder AddWebOrLocalSettingsConfiguration(
            this IConfigurationBuilder builder, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return builder;

            if (url.StartsWith("http"))
            {
                try
                {
                    var stream = new HttpClient().GetStreamAsync(url).GetAwaiter().GetResult();
                    builder.AddJsonStream(stream);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Couldn't download settings from {url}: {ex}");
                }
            }
            else
            {
                try
                {
                    builder.AddJsonFile(url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Couldn't load settings from file {url}: {ex}");
                }
            }
            return builder;
        }
    }
}