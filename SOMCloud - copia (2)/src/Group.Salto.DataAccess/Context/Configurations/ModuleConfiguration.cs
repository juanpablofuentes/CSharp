using System.Collections.Generic;
using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ModuleConfiguration : EntityMappingConfiguration<Module>
    {
        public override void Map(EntityTypeBuilder<Module> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private void Configuration(EntityTypeBuilder<Module> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Key).IsRequired();
            entityTypeBuilder.Property(x => x.Description).IsRequired();
            entityTypeBuilder.HasIndex(x => x.Key).IsUnique();
        }
    }
}
