using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class RolesActionGroupsActionsConfiguration : EntityMappingConfiguration<RolesActionGroupsActions>
    {
        public override void Map(EntityTypeBuilder<RolesActionGroupsActions> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private void Configuration(EntityTypeBuilder<RolesActionGroupsActions> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => new { x.RolId, x.ActionGroupId, x.ActionId });

            entityTypeBuilder.HasOne(r => r.Roles)
                .WithMany(x => x.RolesActionGroupsActions)
                .HasForeignKey(r => r.RolId);

            entityTypeBuilder.HasOne(ag => ag.ActionGroups)
                .WithMany(x => x.RolesActionGroupsActions)
                .HasForeignKey(ag => ag.ActionGroupId);

            entityTypeBuilder.HasOne(a => a.Actions)
                .WithMany(x => x.RolesActionGroupsActions)
                .HasForeignKey(a => a.ActionId);
        }
    }
}