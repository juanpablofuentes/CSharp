using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class InfrastructureConfigurationConfiguration : EntityMappingConfiguration<InfrastructureConfiguration>
    {
        public override void Map(EntityTypeBuilder<InfrastructureConfiguration> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<InfrastructureConfiguration> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Key)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false);
        }
    }
}
