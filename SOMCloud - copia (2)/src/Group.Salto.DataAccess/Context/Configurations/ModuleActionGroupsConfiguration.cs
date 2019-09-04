using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ModuleActionGroupsConfiguration : EntityMappingConfiguration<ModuleActionGroups>
    {
        public override void Map(EntityTypeBuilder<ModuleActionGroups> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private void Configuration(EntityTypeBuilder<ModuleActionGroups> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => new
            {
                e.ModuleId,
                e.ActionGroupsId
            });

            entityTypeBuilder.HasOne(d => d.Module).WithMany(p => p.ModuleActionGroups).HasForeignKey(e => e.ModuleId);
            entityTypeBuilder.HasOne(d => d.ActionGroups).WithMany(p => p.ModuleActionGroups).HasForeignKey(e => e.ActionGroupsId);
        }
    }
}