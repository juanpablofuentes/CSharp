using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class CitiesOtherNamesConfiguration : EntityMappingConfiguration<CitiesOtherNames>
    {
        public override void Map(EntityTypeBuilder<CitiesOtherNames> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<CitiesOtherNames> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => new { e.Id, e.Name });

            entityTypeBuilder.HasIndex(e => e.Name)
                .HasName("UQ__CitiesOt__737584F626077C11")
                .IsUnique();

            entityTypeBuilder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.IdNavigation)
                .WithMany(p => p.CitiesOtherNames)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CitiesOtherN__Id__4E88ABD4");
        }
    }
}
