using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class LanguageConfiguration : EntityMappingConfiguration<Language>
    {
        public override void Map(EntityTypeBuilder<Language> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<Language> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.CultureCode).IsRequired().HasMaxLength(2);
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            entityTypeBuilder.HasIndex(p => p.CultureCode).IsUnique();
        }
    }
}
