using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class WarehouseMovementTypesConfiguration : EntityMappingConfiguration<WarehouseMovementTypes>
    {
        public override void Map(EntityTypeBuilder<WarehouseMovementTypes> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<WarehouseMovementTypes> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
        }
    }
}