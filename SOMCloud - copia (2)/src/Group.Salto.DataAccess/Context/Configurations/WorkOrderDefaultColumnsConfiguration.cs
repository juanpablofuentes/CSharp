using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class WorkOrderDefaultColumnsConfiguration : EntityMappingConfiguration<WorkOrderDefaultColumns>
    {
        public override void Map(EntityTypeBuilder<WorkOrderDefaultColumns> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<WorkOrderDefaultColumns> entityTypeBuilder)
        {
            entityTypeBuilder.HasOne(d => d.WorkOrderColumns)
               .WithMany(p => p.WorkOrderDefaultColumns)
               .HasForeignKey(d => d.WorkOrderColumnId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__WorkOrderColumns__WorkOrderDefaultColumns");
        }
    }
}