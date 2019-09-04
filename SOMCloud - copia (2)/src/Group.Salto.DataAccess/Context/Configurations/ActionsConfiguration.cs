using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ActionsConfiguration: EntityMappingConfiguration<Actions>
    {
        public override void Map(EntityTypeBuilder<Actions> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<Actions> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entityTypeBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
