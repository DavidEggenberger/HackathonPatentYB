using Blazor.Diagrams.Core.Models;
using SharedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Diagrams.Ports;

namespace WebClient.Diagrams.Nodes
{
    public class EnergyRessourceNode : NodeModel
    {
        public EnergyRessourceNode(Blazor.Diagrams.Core.Geometry.Point position = null) : base(position)
        {
            AddPort(new ColumnPort(this, PortAlignment.Bottom));
        }
        public EnergyRessourceDTO Ressource { get; set; }
    }
}
