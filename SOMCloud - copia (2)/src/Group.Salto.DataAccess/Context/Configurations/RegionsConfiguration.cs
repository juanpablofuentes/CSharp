using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class RegionsConfiguration : EntityMappingConfiguration<Regions>
    {
        public override void Map(EntityTypeBuilder<Regions> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<Regions> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.Country)
                .WithMany(p => p.Regions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Regions__Country__52593CB8");
        }
    }
}
