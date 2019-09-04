using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class PredefinedReasonsConfiguration : EntityMappingConfiguration<PredefinedReasons>
    {
        public override void Map(EntityTypeBuilder<PredefinedReasons> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<PredefinedReasons> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.PredefinedDayStates)
                .WithMany(p => p.PredefinedReasons)
                .HasForeignKey(d => d.PredefinedDayStatesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PredefinedReasons_PredefinedDayStates");
        }
    }
}
