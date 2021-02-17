using System;
using System.Linq;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SettingX.Repositories.AzureTableStorage.Settings;

namespace SettingX.Repositories.AzureTableStorage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                int httpPort = 5000;
                Host.CreateDefaultBuilder(args)
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureAppConfiguration(builder =>
                    {
                        var settingsUrl = Environment.GetEnvironmentVariable("SETTINGS_URL");
                        builder.AddWebOrLocalSettingsConfiguration(settingsUrl);
                    })
                    .ConfigureWebHostDefaults(
                        webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                            webBuilder.ConfigureKestrel((ctx, opts) =>
                            {
                                if (ctx.HostingEnvironment.IsDevelopment())
                                {
                                    var urlsString = ctx.Configuration["ASPNETCORE_URLS"];
                                    var urls = urlsString.Split(',', ';', StringSplitOptions.RemoveEmptyEntries);
                                    var url = urls.FirstOrDefault(u => u.StartsWith("http://"));
                                    if (url != null)
                                    {
                                        var parts = url.TrimEnd().Split(':');
                                        if (int.TryParse(parts[parts.Length - 1], out var parsedPort))
                                            httpPort = parsedPort;
                                    }
                                }
                                else
                                {
                                    var httpPortSetting = ctx.Configuration["HttpPort"];
                                    if (!string.IsNullOrWhiteSpace(httpPortSetting)
                                        && int.TryParse(httpPortSetting, out var parsedHttpPort))
                                        httpPort = parsedHttpPort;
                                    webBuilder.UseUrls($"http://*:{httpPort}");
                                }
                            });
                        })
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}