using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class AvailabilityCategoriesConfiguration : EntityMappingConfiguration<AvailabilityCategories>
    {
        public override void Map(EntityTypeBuilder<AvailabilityCategories> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<AvailabilityCategories> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
