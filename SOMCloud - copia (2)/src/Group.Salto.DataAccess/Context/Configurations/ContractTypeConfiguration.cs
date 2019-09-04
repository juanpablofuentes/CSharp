using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ContractTypeConfiguration : EntityMappingConfiguration<ContractType>
    {
        public override void Map(EntityTypeBuilder<ContractType> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<ContractType> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);
        }
    }
}
