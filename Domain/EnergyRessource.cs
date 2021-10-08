using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class EnergyRessource
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime TimeCreated { get; set; }
        public decimal PricePerkWh { get; set; }
        public decimal ProductionDaySunnykWh { get; set; }
        public decimal ProductionDayRainnykWh { get; set; }
        public decimal ProductionNightkWh { get; set; }
        public string ProducerId { get; set; }
        public ApplicationUser Producer { get; set; }
        public string ConsumerId { get; set; }
        public ApplicationUser Consumer { get; set; }
    }
}
