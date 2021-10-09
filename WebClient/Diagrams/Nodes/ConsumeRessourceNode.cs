using Blazor.Diagrams.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Diagrams.Ports;
using WebClient.Components;

namespace WebClient.Diagrams.Nodes
{
    public class ConsumeRessourceNode : NodeModel
    {
        public ConsumeRessourceNode(Blazor.Diagrams.Core.Geometry.Point position = null) : base(position)
        {
            AddPort(new ColumnPort(this, PortAlignment.Bottom));
        }
        public MarketplaceComponent.ConsumeRessource Ressource { get; set; }
    }
}
