using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class OriginsConfiguration : EntityMappingConfiguration<Origins>
    {
        public override void Map(EntityTypeBuilder<Origins> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<Origins> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.Name)
                    .HasName("UK_Origin")
                    .IsUnique();

            entityTypeBuilder.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Observations)
                .HasMaxLength(1000)
                .IsUnicode(false);
        }
    }
}
