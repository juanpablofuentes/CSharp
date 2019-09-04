using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class PostconditionTypesConfiguration : EntityMappingConfiguration<PostconditionTypes>
    {
        public override void Map(EntityTypeBuilder<PostconditionTypes> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<PostconditionTypes> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entityTypeBuilder.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
        }
    }
}