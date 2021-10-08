using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class EnergyRessource
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public decimal ProductionDaySunny { get; set; }
        public decimal ProductionDayRainny { get; set; }
        public decimal ProductionNight { get; set; }
    }
}
