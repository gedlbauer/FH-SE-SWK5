using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderManagement.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManagement.Api.BackgroundServices
{
    public class QueuedUpdateService : BackgroundService
    {
        private readonly IOrderManagementLogic logic;
        private readonly ILogger<QueuedUpdateService> logger;
        private readonly UpdateChannel updateChannel;

        public QueuedUpdateService(IOrderManagementLogic logic, ILogger<QueuedUpdateService> logger, UpdateChannel updateChannel)
        {
            this.logic = logic ?? throw new ArgumentNullException(nameof(logger));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.updateChannel = updateChannel ?? throw new ArgumentNullException(nameof(updateChannel));
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var customerId in updateChannel.ReadAllAsync(stoppingToken))
            {
                await logic.UpdateTotalRevenueAsync(customerId);
                logger.LogInformation($"Updated total revenue for customer");
            }
        }
    }
}
