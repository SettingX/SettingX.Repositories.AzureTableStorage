using Autofac;
using Lykke.Common.Log;
using Lykke.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SettingX.Repositories.AzureTableStorage.Modules;
using SettingX.Repositories.AzureTableStorage.Settings;

namespace SettingX.Repositories.AzureTableStorage
{
    public class Startup
    {
        private const string ApiName = "SettingX.Repositories.AzureTableStorage";
        private readonly AppSettings _appSettings;

        public Startup(IConfiguration configuration)
        {
            _appSettings = AppSettings.Create(configuration); 
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo {
                        Title = ApiName,
                        Version = "v1"
                    });
            });

            services.AddLykkeLogging();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule(_appSettings));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(a =>
            {
                a.RoutePrefix = "swagger/ui";
                a.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
                a.DocumentTitle = $"{ApiName} Service";
                a.EnableDeepLinking();
            });
        }
    }
}