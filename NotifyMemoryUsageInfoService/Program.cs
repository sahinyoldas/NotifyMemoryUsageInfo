using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyMemoryUsageInfoService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var logger = serviceProvider.GetService<ILogger<Program>>();
                    services.AddSingleton(typeof(ILogger), logger);

                    services.AddSingleton<IMailSender, SMTPMailSender>();
                    services.AddSingleton<IMemoryManager, MemoryManager>();
                    services.AddHostedService<Worker>();
                });
    }
}
