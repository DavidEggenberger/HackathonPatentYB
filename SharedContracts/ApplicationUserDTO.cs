using System;
using System.Collections.Generic;
using System.Text;

namespace SharedContracts
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string EnergySupplier { get; set; }
        public decimal Tokens { get; set; }
        public string Address { get; set; }
        public string PictureUri { get; set; }
        public string Email { get; set; } 
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
        public List<EnergyRessourceDTO> EnergyRessourcesProduced { get; set; }
        public List<EnergyRessourceDTO> EnergyRessourcesConsumed { get; set; }
        public List<ApplicationUserFollowerPairDTO> UsersFollowing { get; set; }
        public List<ApplicationUserFollowerPairDTO> UsersFollowedBy { get; set; }
    }
}
