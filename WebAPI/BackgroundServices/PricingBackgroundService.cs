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
using Domain;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.BackgroundServices
{
    public class PricingBackgroundService : BackgroundService
    {
        private IServiceProvider serviceProvider;
        private IHubContext<MarketHub> priceHub;
        public PricingBackgroundService(IServiceProvider serviceProvider, IHubContext<MarketHub> priceHub)
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
                await Task.Delay(10000);

                List<ApplicationUser> applicationUsers = applicationDbContext.Users
                                                            .Include(x => x.EnergyRessourcesConsumed)
                                                            .ThenInclude(x => x.Producer)
                                                            .ToList();
                foreach (var user in applicationUsers)
                {
                    if(user.EnergyRessourcesConsumed.Count > 0)
                    {
                        user.Tokens -= (decimal)new double[] { 0.02, 0.001, 0.01 }[new Random().Next(0, 2)];
                    }
                    //foreach (var ressource in user.EnergyRessourcesConsumed)
                    //{
                    //    if (DateTime.Now.DayOfYear < DateTime.Now.AddDays(ressource.DurationInDays).DayOfYear - DateTime.Now.DayOfYear)
                    //    {
                    //        decimal pricePerSec = ressource.PricePerkWh / (60 * 60);
                    //        decimal average = (ressource.ProductionDayRainnykWh + ressource.ProductionDaySunnykWh + ressource.ProductionNightkWh) / 3;
                    //        decimal cost = average * pricePerSec;
                    //        user.Tokens -= cost;
                    //        ressource.Producer.Tokens += cost;
                    //    }
                    //}
                }
                await applicationDbContext.SaveChangesAsync();
                await priceHub.Clients.All.SendAsync("Update");
            }
        }
    }
}
