using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class StatesConfiguration : EntityMappingConfiguration<States>
    {
        public override void Map(EntityTypeBuilder<States> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<States> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.Region)
                .WithMany(p => p.States)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__States__RegionId__534D60F1");
        }
    }
}
