using Blazor.Diagrams.Core.Models;
using SharedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Diagrams.Ports;

namespace WebClient.Diagrams.Nodes
{
    public class UserNode : NodeModel
    {
        public ApplicationUserDTO ApplicationUser { get; set; }
        private HttpClient httpClient;
        public UserNode(HttpClient httpClient, Blazor.Diagrams.Core.Geometry.Point position = null) : base(position)
        {
            AddPort(new ColumnPort(this, PortAlignment.Bottom));
            this.httpClient = httpClient;
        }
        public async Task ConsumeRessource(EnergyRessourceDTO energyRessourceDTO)
        {

        }
    }
}
