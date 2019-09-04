using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Common
{
    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder modelBuilder);
    }

    public interface IEntityMappingConfiguration<TEntity> : IEntityMappingConfiguration where TEntity : class
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}
