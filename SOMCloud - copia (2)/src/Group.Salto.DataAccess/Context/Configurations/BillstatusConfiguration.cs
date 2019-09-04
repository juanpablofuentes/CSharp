using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class BillStatusConfiguration : EntityMappingConfiguration<BillStatus>
    {
        public override void Map(EntityTypeBuilder<BillStatus> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<BillStatus> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.BillStatusId)
                    .IsRequired();
            entityTypeBuilder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
