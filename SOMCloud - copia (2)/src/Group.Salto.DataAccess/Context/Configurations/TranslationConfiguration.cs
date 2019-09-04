using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class TranslationConfiguration : EntityMappingConfiguration<Translation>
    {
        public override void Map(EntityTypeBuilder<Translation> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<Translation> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Key).IsRequired();
            entityTypeBuilder.Property(x => x.Text).IsRequired();
            entityTypeBuilder.HasOne(x => x.Language)
                             .WithMany(x => x.Translations)
                             .HasForeignKey(l => l.LanguageId).IsRequired();
            entityTypeBuilder.HasIndex(x => new { x.Key, x.LanguageId }).IsUnique();
        }
    }
}
