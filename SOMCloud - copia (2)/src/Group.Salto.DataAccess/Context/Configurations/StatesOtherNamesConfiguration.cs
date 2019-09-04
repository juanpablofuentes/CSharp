using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class StatesOtherNamesConfiguration : EntityMappingConfiguration<StatesOtherNames>
    {
        public override void Map(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StatesOtherNames> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StatesOtherNames> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => new { e.Id, e.Name });

            entityTypeBuilder.HasIndex(e => e.Name)
                .HasName("UQ__StatesOt__737584F64516A7F1")
                .IsUnique();

            entityTypeBuilder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.IdNavigation)
                .WithMany(p => p.StatesOtherNames)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StatesOtherN__Id__5441852A");
        }
    }
}
