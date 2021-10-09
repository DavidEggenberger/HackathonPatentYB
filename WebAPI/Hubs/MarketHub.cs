using Domain;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.SignalR;
using SharedContracts.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Hubs
{
    public class MarketHub : Hub
    {
        private ApplicationDbContext applicationDbContext;
        public MarketHub(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task BuySell(BuySellOrderDTO buySellOrderDTO)
        {
            Market market = applicationDbContext.Markets.First();
            market.Supplied += buySellOrderDTO.Supplied;
            market.Demanded += buySellOrderDTO.Demanded;
            await applicationDbContext.SaveChangesAsync();
            await Clients.All.SendAsync("MarketUpdate", new MarketDTO { Demanded = market.Demanded, Supplied = market.Supplied, MovingUp = buySellOrderDTO.Demanded > buySellOrderDTO.Supplied ? true : false });
        }
    }
}
