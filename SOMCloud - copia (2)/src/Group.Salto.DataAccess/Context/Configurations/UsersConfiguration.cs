using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class UsersConfiguration : EntityMappingConfiguration<Users>
    {
        public override void Map(EntityTypeBuilder<Users> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private void Configuration(EntityTypeBuilder<Users> entityTypeBuilder)
        {
            entityTypeBuilder.HasOne(x => x.Customer)
                             .WithMany(x => x.Users)
                             .HasForeignKey(l => l.CustomerId);

            entityTypeBuilder.HasOne(x => x.Language)
                             .WithMany(x => x.Users)
                             .HasForeignKey(l => l.LanguageId);
        }
    }
}
