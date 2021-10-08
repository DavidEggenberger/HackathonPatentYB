using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ApplicationUserFollowerPairConfiguration : IEntityTypeConfiguration<ApplicationUserFollowerPair>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserFollowerPair> builder)
        {
            builder.HasOne(x => x.Followed).WithMany(x => x.UsersFollowedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Follower).WithMany(x => x.UsersFollowing).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
