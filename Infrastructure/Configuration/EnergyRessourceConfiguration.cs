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
    public class EnergyRessourceConfiguration : IEntityTypeConfiguration<EnergyRessource>
    {
        public void Configure(EntityTypeBuilder<EnergyRessource> builder)
        {
            builder.HasOne(x => x.Producer).WithMany(x => x.EnergyRessourcesProduced).HasForeignKey(x => x.ProducerId).OnDelete(DeleteBehavior.NoAction).IsRequired(true);
            builder.HasOne(x => x.Consumer).WithMany(x => x.EnergyRessourcesConsumed).HasForeignKey(x => x.ConsumerId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        }
    }
}
