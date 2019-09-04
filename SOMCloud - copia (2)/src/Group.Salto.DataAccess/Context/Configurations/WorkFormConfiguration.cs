using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class WorkFormConfiguration : EntityMappingConfiguration<WorkForm>
    {
        public override void Map(EntityTypeBuilder<WorkForm> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<WorkForm> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Id)
                    .IsRequired();
            entityTypeBuilder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
