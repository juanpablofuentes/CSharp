using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ICollectionsExtraFieldExtraFieldRepository : IRepository<CollectionsExtraFieldExtraField>
    {
        CollectionsExtraFieldExtraField DeleteOnContext(CollectionsExtraFieldExtraField entity);
    }
}