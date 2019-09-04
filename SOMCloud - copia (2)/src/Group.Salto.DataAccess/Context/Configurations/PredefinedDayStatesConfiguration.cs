using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class PredefinedDayStatesConfiguration : EntityMappingConfiguration<PredefinedDayStates>
    {
        public override void Map(EntityTypeBuilder<PredefinedDayStates> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<PredefinedDayStates> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
