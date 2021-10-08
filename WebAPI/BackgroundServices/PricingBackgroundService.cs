using Infrastructure.Persistance;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using WebAPI.Hubs;

namespace WebAPI.BackgroundServices
{
    public class PricingBackgroundService : BackgroundService
    {
        private IServiceProvider serviceProvider;
        private IHubContext<PriceHub> priceHub;
        public PricingBackgroundService(IServiceProvider serviceProvider, IHubContext<PriceHub> priceHub)
        {
            this.serviceProvider = serviceProvider;
            this.priceHub = priceHub;
        }
        protected override async Task ExecuteAsync(System.Threading.CancellationToken stoppingToken)
        {
            using IServiceScope iServiceScope = serviceProvider.CreateScope();
            ApplicationDbContext applicationDbContext = iServiceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);

            }
        }
    }
}
