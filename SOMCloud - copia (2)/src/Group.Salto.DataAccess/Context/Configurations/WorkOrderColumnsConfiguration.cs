using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class WorkOrderColumnsConfiguration : EntityMappingConfiguration<WorkOrderColumns>
    {
        public override void Map(EntityTypeBuilder<WorkOrderColumns> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<WorkOrderColumns> entityTypeBuilder)
        {
            entityTypeBuilder.Property(c => c.Id).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

            entityTypeBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}