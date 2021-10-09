using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.BackgroundServices;
using Infrastructure.Persistance;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            using IServiceScope serviceScope = host.Services.CreateScope();
            ApplicationDbContext appDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if(appDbContext.Markets.Count() == 0)
            {
                appDbContext.Markets.Add(new Domain.Market
                {
                    
                });
                appDbContext.SaveChanges();
            }
            //if(appDbContext.EnergyRessources.Count() == 0)
            //{
            //    appDbContext.EnergyRessources.Add(new Domain.EnergyRessource
            //    {
            //        PricePerkWh = 30,
            //        ProductionDayRainnykWh = 30,
            //        ProductionNightkWh = 9,
            //        ProductionDaySunnykWh = 3,
            //        Source = "Solar",
            //    });
            //    appDbContext.SaveChanges();
            //}

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {        
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddAzureKeyVault(new Uri("https://justrollkeyvault.vault.azure.net/"),
                            new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "466d4d5f-bbac-4123-8b10-905e2fc51cb5" }));
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddHostedService<PricingBackgroundService>();
                    });
                });
    }
}
