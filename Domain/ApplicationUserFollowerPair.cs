using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ApplicationUserFollowerPair
    {
        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }
        public string FollowedId { get; set; }
        public ApplicationUser Followed { get; set; }
    }
}
