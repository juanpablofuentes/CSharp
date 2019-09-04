using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ActionsRolesConfiguration : EntityMappingConfiguration<ActionsRoles>
    {
        public override void Map(EntityTypeBuilder<ActionsRoles> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<ActionsRoles> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(e => new
            {
                e.RoleId,
                e.ActionId
            });

            //TODO: Carmen. RolesActionGroupsActions
            entityTypeBuilder.HasOne(d => d.Roles).WithMany(p => p.ActionsRoles).HasForeignKey(e => e.RoleId);
            entityTypeBuilder.HasOne(d => d.Actions).WithMany(p => p.ActionsRoles).HasForeignKey(e => e.ActionId);
        }
    }
}
