using DataAccess.Common;
using Group.Salto.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class TrackerConfiguration : EntityMappingConfiguration<Tracker>
    {
        public override void Map(EntityTypeBuilder<Tracker> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private void Configuration(EntityTypeBuilder<Tracker> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.EntityType).IsRequired();
            entityTypeBuilder.Property(x => x.PropertyName).IsRequired();
            entityTypeBuilder.Property(x => x.TimeStamp).IsRequired();
            entityTypeBuilder.Property(x => x.OldValue).IsRequired();
            entityTypeBuilder.Property(x => x.NewValue).IsRequired();
            entityTypeBuilder.Property(x => x.EntityId).IsRequired();
            entityTypeBuilder.Property(x => x.OwnerId).IsRequired();
            entityTypeBuilder.Property(x => x.TransactionId).IsRequired();
        }
    }
}
