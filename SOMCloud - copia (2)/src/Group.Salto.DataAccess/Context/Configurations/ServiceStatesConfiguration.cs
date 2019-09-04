using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ServiceStatesConfiguration : EntityMappingConfiguration<ServiceStates>
    {
        public override void Map(EntityTypeBuilder<ServiceStates> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<ServiceStates> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
