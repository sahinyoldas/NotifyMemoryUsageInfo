using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotifyMemoryUsageInfoService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IMemoryManager _memoryManager;

        public Worker(ILogger<Worker> logger, IMemoryManager memoryManager)
        {
            _logger = logger;
            _memoryManager = memoryManager;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.WriteToFile(ConstantFields.SystemText.ServiceStartText + DateTime.Now + Environment.NewLine + ConstantFields.SystemText.NewLineSymbolsText);
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _memoryManager.CheckMemoryStatus();
                await Task.Delay(ConstantFields.ServiceWorkingTime, stoppingToken);
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.WriteToFile(ConstantFields.SystemText.ServiceStopedText + DateTime.Now + " ***" + Environment.NewLine);
            await base.StopAsync(cancellationToken);
        }
    }
}
