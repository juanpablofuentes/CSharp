using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class CalendarEventCategoriesConfiguration : EntityMappingConfiguration<CalendarEventCategories>
    {
        public override void Map(EntityTypeBuilder<CalendarEventCategories> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<CalendarEventCategories> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Color)
                    .HasMaxLength(8)
                    .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.AvailabilityCategories)
                .WithMany(p => p.CalendarEventCategories)
                .HasForeignKey(d => d.AvailabilityCategoriesId)
                .HasConstraintName("FK_CalendarEventCategories_AvailabilityCategories");
        }
    }
}
