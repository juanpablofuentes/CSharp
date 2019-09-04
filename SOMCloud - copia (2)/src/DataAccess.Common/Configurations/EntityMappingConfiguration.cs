using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Common
{
    public abstract class EntityMappingConfiguration<TEntity> : IEntityMappingConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> entityTypeBuilder);

        public void Map(ModelBuilder modelBuilder)
        {
            Map(modelBuilder.Entity<TEntity>());
        }
    }
}
