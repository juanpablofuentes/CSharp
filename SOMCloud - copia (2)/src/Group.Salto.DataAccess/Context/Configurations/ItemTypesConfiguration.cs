using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ItemTypesConfiguration : EntityMappingConfiguration<ItemTypes>
    {
        public override void Map(EntityTypeBuilder<ItemTypes> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<ItemTypes> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        }        
    }
}