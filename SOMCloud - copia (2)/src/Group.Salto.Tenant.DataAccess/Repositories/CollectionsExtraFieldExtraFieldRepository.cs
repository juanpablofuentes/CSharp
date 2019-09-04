using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class CollectionsExtraFieldExtraFieldRepository : BaseRepository<CollectionsExtraFieldExtraField>, ICollectionsExtraFieldExtraFieldRepository
    {
        public CollectionsExtraFieldExtraFieldRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public CollectionsExtraFieldExtraField DeleteOnContext(CollectionsExtraFieldExtraField entity)
        {
            Delete(entity);
            return entity;
        }
    }
}