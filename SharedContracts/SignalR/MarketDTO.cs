using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedContracts.SignalR
{
    public class MarketDTO
    {
        public bool MovingUp { get; set; }
        public decimal Supplied { get; set; }
        public decimal Demanded { get; set; }
    }
}
