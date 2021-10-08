using System;

namespace SharedContracts
{
    public class EnergyRessourceDTO
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
        public decimal ProductionDaySunny { get; set; }
        public decimal ProductionDayRainny { get; set; }
        public decimal ProductionNight { get; set; }
        public string ProducerId { get; set; }
        public string ConsumerId { get; set; }
    }
}