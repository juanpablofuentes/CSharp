using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ActionGroupsConfiguration : EntityMappingConfiguration<ActionGroups>
    {
        public override void Map(EntityTypeBuilder<ActionGroups> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<ActionGroups> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}