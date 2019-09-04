using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class PostalCodesConfiguration : EntityMappingConfiguration<PostalCodes>
    {
        public override void Map(EntityTypeBuilder<PostalCodes> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<PostalCodes> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityTypeBuilder.Property(e => e.PostalCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.HasOne(d => d.IdNavigation)
                .WithMany(p => p.PostalCodes)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostalCodes__Id__5070F446");
        }
    }
}
