using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class CustomerModuleConfiguration: EntityMappingConfiguration<CustomerModule>
    {
        public override void Map(EntityTypeBuilder<CustomerModule> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private void Configuration(EntityTypeBuilder<CustomerModule> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(cm => new { cm.CustomerId, cm.ModuleId });

            entityTypeBuilder.HasOne(bc => bc.Customer)
                .WithMany(b => b.ModulesAssigned)
                .HasForeignKey(bc => bc.CustomerId);

            entityTypeBuilder.HasOne(bc => bc.Module)
                .WithMany(b => b.CustomersAssigned)
                .HasForeignKey(bc => bc.ModuleId);
        }
    }
}
