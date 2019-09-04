using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class CountriesConfiguration : EntityMappingConfiguration<Countries>
    {
        public override void Map(EntityTypeBuilder<Countries> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<Countries> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
        }
    }
}
