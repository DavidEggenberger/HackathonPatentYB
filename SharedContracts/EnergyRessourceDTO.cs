using System;

namespace SharedContracts
{
    public class EnergyRessourceDTO
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public string Location { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal PricekWh { get; set; }
        public decimal ProductionDaySunnykWh { get; set; }
        public decimal ProductionDayRainnykWh { get; set; }
        public decimal ProductionNightkWh { get; set; }
        public string ProducerId { get; set; }
        public string ConsumerId { get; set; }
    }
}