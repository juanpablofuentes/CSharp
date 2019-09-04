using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class CitiesConfiguration : EntityMappingConfiguration<Cities>
    {
        public override void Map(EntityTypeBuilder<Cities> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<Cities> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.Municipality)
                .WithMany(p => p.Cities)
                .HasForeignKey(d => d.MunicipalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cities__Municipa__4D94879B");
        }
    }
}
