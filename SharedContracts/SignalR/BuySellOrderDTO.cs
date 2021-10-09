using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedContracts.SignalR
{
    public class BuySellOrderDTO
    {
        public decimal Demanded { get; set; }
        public decimal Supplied { get; set; }
    }
}
