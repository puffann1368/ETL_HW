using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using ETLProcess.Interface;
using ETLProcess.Interface.IRepository;

namespace ETLProcess
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConfiguration _configuration { get; }
        private readonly IServiceProvider _sp;

        private IProductionRepo _IProdRepo;

        private ILineItemRepo _ILineItemRepo;

        public Worker(ILogger<Worker> logger,IConfiguration configuration,IServiceProvider sp,IProductionRepo IProdRepo,ILineItemRepo ILineItemRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _sp = sp;
            _IProdRepo = IProdRepo;
            _ILineItemRepo = ILineItemRepo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //only start at 4 p.m. 
            bool IsRun = false;
            while (!stoppingToken.IsCancellationRequested && !IsRun)
            {
                string Source_Link = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "{input Source Path}");
                            
                ETLProcess.Services.Processor Proceess = new ETLProcess.Services.Processor(_logger, _IProdRepo,_ILineItemRepo);
                await Proceess.Run(Source_Link,stoppingToken);
                _logger.LogInformation("complete.");
                IsRun = true;
            }
        }
    }
}
