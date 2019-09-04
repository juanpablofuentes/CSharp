using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class CalculationTypeConfiguration : EntityMappingConfiguration<CalculationType>
    {
        public override void Map(EntityTypeBuilder<CalculationType> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }
        private static void Configuration(EntityTypeBuilder<CalculationType> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}