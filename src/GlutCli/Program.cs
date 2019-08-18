using Glut;
using Glut.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System;
using Glut.Providers;
using Glut.Interface;
using Microsoft.Extensions.Options;
using Glut.Data;
using Microsoft.EntityFrameworkCore;

namespace GlutCli
{
    /// <summary>
    /// Run from powershell .\Run.ps1
    /// -basePath=C:\Temp
    /// </summary>
    class Program
    {
        public static async Task Main(string[] args)
        {
            IHostBuilder builder = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                 {
                     config.Properties.Add(HostDefaults.ApplicationKey, GlutConstants.ApplicationName);
                     config.SetBasePath(Path.Combine(AppContext.BaseDirectory));

                     config.AddJsonFile("hostsettings.json", optional: false);
                     config.AddEnvironmentVariables();
                 })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    hostingContext.HostingEnvironment.ApplicationName = GlutConstants.ApplicationName;
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));
                    var path = hostContext.Configuration.GetValue<string>("AppConfig:ContentRootPath");

                    IFileProvider physicalProvider = new PhysicalFileProvider(path);
                    services.AddSingleton<IFileProvider>(physicalProvider);

                    services.AddSingleton<Runner>();
                    services.AddSingleton<HttpClient>();
                    services.AddSingleton<ThreadResult>();
                    services.AddHostedService<LoadBackgroundService>();
                    services.AddSingleton<IWorker, HttpClientService>();
                    services.AddSingleton(CompositeRequestMessageProviderFactory);
                    services.AddTransient<IResultStore, EfResultStore>();
                    services.AddSingleton<IEnvironment, EnvironmentService>();
                    services.AddDbContext<EfDbContext>(options => 
                    options.UseSqlite(hostContext.Configuration.GetConnectionString("EfDbContext")), ServiceLifetime.Transient);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            var host = builder.Build();
            await host.RunAsync();
        }

        private static CompositeRequestMessageProvider CompositeRequestMessageProviderFactory(IServiceProvider service)
        {
            var factory = service.GetService<ILoggerFactory>();
            var lLogger = factory.CreateLogger<ListHttpRequestMessageProvider>();
            var sLogger = factory.CreateLogger<SingleRequestMessageProvider>();

            // NOTE: The default order is List is first then Single request those are sorted by fileName ASC
            var fileProvider = service.GetService<IFileProvider>();
            var list = new ListHttpRequestMessageProvider("List", fileProvider, lLogger);
            var single = new SingleRequestMessageProvider("Single", fileProvider, sLogger);

            return new CompositeRequestMessageProvider(list, single);
        }
    }
}
