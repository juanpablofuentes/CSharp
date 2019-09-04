using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ICollectionsExtraFieldRepository : IRepository<CollectionsExtraField>
    {
        Dictionary<int, string> GetAllKeyValues();
        IQueryable<CollectionsExtraField> GetAll();
        CollectionsExtraField GetById(int id);
        CollectionsExtraField GetByIdWithExtraFields(int id);
        SaveResult<CollectionsExtraField> UpdateCollectionsExtraField(CollectionsExtraField entity);
        SaveResult<CollectionsExtraField> CreateCollectionsExtraField(CollectionsExtraField entity);
        SaveResult<CollectionsExtraField> DeleteCollectionsExtraField(CollectionsExtraField entity);
        CollectionsExtraField GetByIdWithPredefinedServices(int id);        
    }
}