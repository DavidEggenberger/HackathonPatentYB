using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Market
    {
        public Guid Id { get; set; }
        public bool MovingUp { get; set; }
        public decimal Supplied { get; set; }
        public decimal Demanded { get; set; }
    }
}
