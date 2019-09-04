using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class RepetitionTypesConfiguration : EntityMappingConfiguration<RepetitionTypes>
    {
        public override void Map(EntityTypeBuilder<RepetitionTypes> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<RepetitionTypes> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedNever();

            entityTypeBuilder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
