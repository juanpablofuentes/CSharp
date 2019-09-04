using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class DaysTypeConfiguration : EntityMappingConfiguration<DaysType>
    {
        public override void Map(EntityTypeBuilder<DaysType> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }
        private static void Configuration(EntityTypeBuilder<DaysType> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}