using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class DamagedEquipmentConfiguration : EntityMappingConfiguration<DamagedEquipment>
    {
        public override void Map(EntityTypeBuilder<DamagedEquipment> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }
        private static void Configuration(EntityTypeBuilder<DamagedEquipment> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Id)
                    .IsRequired();

            entityTypeBuilder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}