using Microsoft.AspNetCore.SignalR;
using SharedContracts.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Hubs
{
    public class PriceHub : Hub
    {
        public async Task BuySell(BuySellOrderDTO buySellOrderDTO)
        {
            //await Clients.All.SendAsync("MarketUpdate", );
        }
    }
}
