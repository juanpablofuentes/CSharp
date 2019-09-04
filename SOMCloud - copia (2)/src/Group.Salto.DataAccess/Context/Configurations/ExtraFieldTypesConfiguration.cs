using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ExtraFieldTypesConfiguration : EntityMappingConfiguration<ExtraFieldTypes>
    {
        public override void Map(EntityTypeBuilder<ExtraFieldTypes> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<ExtraFieldTypes> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        }        
    }
}