using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string EnergySupplier { get; set; }
        public decimal Tokens { get; set; }
        public string Address { get; set; }
        public string PictureUri { get; set; }
        public decimal Need { get; set; }
        public List<EnergyRessource> EnergyRessourcesProduced { get; set; }
        public List<EnergyRessource> EnergyRessourcesConsumed { get; set; }
        public List<ApplicationUserFollowerPair> UsersFollowing { get; set; }
        public List<ApplicationUserFollowerPair> UsersFollowedBy { get; set; }
    }
}
