using Blazor.Diagrams.Core.Models;
using SharedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Components;
using WebClient.Diagrams.Ports;

namespace WebClient.Diagrams.Nodes
{
    public class UserNode : NodeModel
    {
        public ApplicationUserDTO ApplicationUser { get; set; }
        private HttpClient httpClient;
        public UserNode(HttpClient httpClient, Blazor.Diagrams.Core.Geometry.Point position = null) : base(position)
        {
            AddPort(new ColumnPort(this, PortAlignment.Top));
            AddPort(new ColumnPort(this, PortAlignment.Bottom));
            this.httpClient = httpClient;
        }
        public event Func<decimal, Task> Update;
        public decimal kWh { get; set; }
        public void BuyRessource(EnergyRessourceDTO energyRessourceDTO)
        {
            kWh += energyRessourceDTO.ProductionDaySunnykWh;
            Update?.Invoke((energyRessourceDTO.ProductionDayRainnykWh + energyRessourceDTO.ProductionDaySunnykWh + energyRessourceDTO.ProductionNightkWh) / 3);
        }
        public void ConsumeRessource(MarketplaceComponent.ConsumeRessource energyRessourceDTO)
        {
            kWh -= energyRessourceDTO.kWh;
            Update?.Invoke(- energyRessourceDTO.kWh);
        }
    }
}
